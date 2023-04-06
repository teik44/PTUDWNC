using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Collections;
using TatBlog.Core.Constants;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints
{
    public static class CategoryEndPoints
    {
        public static WebApplication MapCategoryEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");
            routeGroupBuilder.MapGet("/", GetCategory)
                 .WithName("GetCategory")
                 .Produces<PaginationResult<CategoryItem>>();

            routeGroupBuilder.MapGet("/{id:int}", GetCategoryDetails)
                .WithName("GetCategoryDetails")
                .Produces<CategoryItem>()
                .Produces(404);

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9-]+$)}/posts", GetPostsByCategorySlug)
                .WithName("GetPostsByCategorySlug")
                .Produces<PaginationResult<CategoryDto>>();


            routeGroupBuilder.MapDelete("/{id:int}", DeleteCategory)
                .WithName("DeleteCategory")
                .Produces(204)
                .Produces(404);

            routeGroupBuilder.MapPost("/", AddCategory)
                .WithName("AddNewCategory")
                .AddEndpointFilter<ValidatorFilter<CategoryEditModel>>()
                .Produces(201)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapPut("/{id:int}", UpdateCategory)
              .WithName("UpdateCaCategory")
              .AddEndpointFilter<ValidatorFilter<CategoryEditModel>>()
              .Produces(204)
              .Produces(400)
              .Produces(409);


            return app;
        }
        private static async Task<IResult> GetCategory(
           [AsParameters] CategoryFilterModel model,
           IBlogRepository blogRepository)
        {
            var categoryList = await blogRepository
                .GetPagedCategoryAsync(model, model.Name);
            var paginationResult = new PaginationResult<CategoryItem>(categoryList);
            return Results.Ok(paginationResult);
        }


        private static async Task<IResult> GetCategoryDetails(
            int id,
            IBlogRepository blogRepository,
            IMapper mapper)
        {
            var category = await blogRepository.GetCachedCategoryByIdAsync(id);
            return category == null
                ? Results.NotFound($"khong tim thay tac gia co ma so {id}")
                : Results.Ok(mapper.Map<CategoryItem>(category));
        }
        //
        private static async Task<IResult> GetPostsByCategorySlug(
           [FromRoute] string slug,
           [AsParameters] PagingModel pagingModel,
           IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                CategorySlug = slug,
                PublishedOnly = true,
            };
            var postsList = await blogRepository.GetPagedPostsAsync(
                postQuery, pagingModel,
                posts => posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);
            return Results.Ok(paginationResult);
        }
        /////
        private static async Task<IResult> AddCategory(
          CategoryEditModel model,
          IValidator<CategoryEditModel> validator,
          IBlogRepository blogRepository,
          IMapper mapper)
        {
            
            if (await blogRepository.IsCategorySlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Conflict($"Slug'{model.UrlSlug}' da duoc su dung");
            }
            var category = mapper.Map<Category>(model);
            await blogRepository.AddOrUpdateAsync(category);
            return Results.CreatedAtRoute(
                "GetCategoryById", new { category.Id },
                mapper.Map<CategoryItem>(category));

        }
        //delete
        private static async Task<IResult> DeleteCategory(
           int id, IBlogRepository blogRepository)
        {
            return await blogRepository.DeleteCategoryAsync(id)
                ? Results.NoContent()
                : Results.NotFound($"could not find author with id={id}");
        }
        //update
        private static async Task<IResult> UpdateCategory(
           int id, CategoryEditModel model,
           IValidator<CategoryEditModel> validator,
           IBlogRepository blogRepository,
           IMapper mapper)
        {
            //var validationResult = await validator.ValidateAsync(model);
            //if (!validationResult.IsValid)
            //{
            //    return Results.BadRequest(
            //        validationResult.Errors.ToResponse());
            //}

            if (await blogRepository
                .IsCategorySlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Conflict(
                    $"Slug '{model.UrlSlug}' da duoc su dung");
            }
            var category = mapper.Map<Category>(model);
            category.Id = id;

            return await blogRepository.AddOrUpdateAsync(category)
                ? Results.NoContent()
                : Results.NotFound();
        }


    }
}
