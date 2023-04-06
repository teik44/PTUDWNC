using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.Constants;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;
using TatBlog.WebApp.Validations;

public class PostsController : Controller
{
    private readonly IBlogRepository _blogRepository;
    private readonly IValidator<PostEditModel> _postValidator;
    private readonly IMediaManager _mediaManager;
    private readonly IMapper _mapper;
    private IValidator<PostEditModel>? postValidator;
    private readonly ILogger<PostsController> _logger;

    public PostsController(IBlogRepository blogRepository, IMapper mapper, IMediaManager mediaManager,
        ILogger<PostsController> logger)

    {
        _logger = logger;
        _postValidator = postValidator;
        _blogRepository = blogRepository;
        _mediaManager = mediaManager;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id = 0)
    {
        //ID = 0 them bai viet moi
        //ID > 0 Doc du lieu cua bai viet tu csdl
        var post = id > 0
            ? await _blogRepository.GetPostByIdAsync(id, true)
            : null;
        //tao view model tu du lieu cua bai viet
        var model = post == null
            ? new PostEditModel()
            : _mapper.Map<PostEditModel>(post);

        await PopulatePostEditModelAsync(model);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> VerifyPostSlug(int id, string urlSlug)
    {
        var slugExisted = await _blogRepository
            .IsPostSlugExistedAsync(id, urlSlug);
        return slugExisted
            ? Json($"Slug'{urlSlug}' da duoc su dung")
            : Json(true);
    }



    [HttpPost]
    public async Task<IActionResult> Edit(IValidator<PostEditModel>postValidator, PostEditModel model)
    {
        var validationResult = await _postValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
        }

        if (!ModelState.IsValid)
        {
            await PopulatePostEditModelAsync(model);
            return View(model);
        }
        var post = model.Id > 0
            ? await _blogRepository.GetPostByIdAsync(model.Id)
            : null;
        if (post == null)
        {
            post = _mapper.Map<Post>(model);
            post.Id = 0;
            post.PostedDate = DateTime.Now;
        }
        else
        {
            _mapper.Map(model, post);
            post.Category = null;
            post.ModifiedDate = DateTime.Now;

        }

        if (model.ImageFile?.Length > 0)
        {
            var newImagePath = await _mediaManager.SaveFileAsync(
                model.ImageFile.OpenReadStream(),
                model.ImageFile.FileName,
                model.ImageFile.ContentType);
            if (!string.IsNullOrWhiteSpace(newImagePath))
            {
                await _mediaManager.DeleteFileAsync(post.ImageUrl);
                post.ImageUrl = newImagePath;
            }
        }
        await _blogRepository.CreateOrUpdatePostAsync(post, model.GetSelectedTags());

        return RedirectToAction(nameof(Index));
    }



    private async Task PopulatePostEditModelAsync(PostEditModel model)
    {
        var authors = await _blogRepository.GetAuthorsAsync();

        var categories = await _blogRepository.GetCategoriesAsync();

        model.AuthorList = authors.Select(a => new SelectListItem()
        {
            Text = a.FullName,
            Value = a.Id.ToString()
        });
        model.CategoryList = categories.Select(c => new SelectListItem()
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
    }
    //
    

    private async Task PopulatePostFilterModelAsync(PostFilterModel model)
    {
        var authors = await _blogRepository.GetAuthorsAsync();

        var categories = await _blogRepository.GetCategoriesAsync();

        model.AuthorList = authors.Select(a => new SelectListItem()
        {
            Text = a.FullName,
            Value = a.Id.ToString()
        });
        model.CategoryList = categories.Select(c => new SelectListItem()
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
    }
    public async Task<IActionResult> Index(PostFilterModel model)
    {
        //var postQuery = new PostQuery()

        //{
        //   Keyword= model.Keyword,
        //   CategoryId = model.CategoryId,
        //   AuthorId = model.AuthorId,
        //    PostedYear = model.Year,
        //  PostedMonth  = model.Month
        //};
        var postQuery = _mapper.Map<PostQuery>(model);

        ViewBag.PostsList = await _blogRepository
            .GetPagedPostsAsync(postQuery, 1, 10);

        await PopulatePostFilterModelAsync(model);
        return View(model);
    }
}  