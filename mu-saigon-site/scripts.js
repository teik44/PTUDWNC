const icons = {
  info: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><circle cx="12" cy="12" r="9"/><line x1="12" y1="8" x2="12" y2="12"/><circle cx="12" cy="16" r="0.8"/></svg>',
  reset: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><polyline points="1 4 1 10 7 10"/><polyline points="23 20 23 14 17 14"/><path d="M3.51 15a9 9 0 0 0 14.13 3.36L23 10"/><path d="M1 14l5-5a9 9 0 0 1 14.49-2.13"/></svg>',
  event: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><rect x="3" y="4" width="18" height="18" rx="2"/><path d="M16 2v4"/><path d="M8 2v4"/><path d="M3 10h18"/></svg>',
  timeline: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><path d="M12 5v14"/><circle cx="12" cy="5" r="1.5"/><circle cx="12" cy="12" r="1.5"/><circle cx="12" cy="19" r="1.5"/></svg>',
  guide: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><path d="M7 20h10"/><path d="M12 4v16"/><path d="M8 4h8a2 2 0 0 1 2 2v6H6V6a2 2 0 0 1 2-2z"/></svg>',
  donate: '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><path d="M12 1v22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7H14a3.5 3.5 0 0 1 0 7H6"/></svg>'
};

const gameSections = [
  {
    id: 'server-info',
    title: 'Thông tin server',
    subtitle: 'Season 2.2',
    icon: 'info',
    type: 'stats',
    description: 'Máy chủ MU Sài Gòn giữ nguyên cảm giác MU Việt 2008 với điều chỉnh cân bằng chỉ số để PK bùng nổ.',
    stats: [
      { label: 'Season', value: '2.2 Classic' },
      { label: 'Exp', value: 'x200 solo / x250 party' },
      { label: 'Drop', value: '50% Custom' },
      { label: 'Anti', value: 'ServerGuard + Camera 24/7' },
      { label: 'Reset giới hạn', value: '120 lần' },
      { label: 'Đồ đạc', value: 'Item Thiên Sứ + Socket cổ điển' }
    ],
    note: 'Giới hạn 2 account/PC, PK mở toàn thời gian trừ khu vực bảo vệ tân thủ.'
  },
  {
    id: 'reset-info',
    title: 'Thông tin reset',
    subtitle: 'Cột mốc rõ ràng',
    icon: 'reset',
    type: 'table',
    headers: ['Cột mốc reset', 'Level yêu cầu', 'Nguyên liệu', 'Point tặng'],
    rows: [
      ['01 - 20', 'Lv 350', '3 Jewel of Soul', '500 point/lần'],
      ['21 - 50', 'Lv 370', '3 Soul + 1 Creation', '600 point/lần'],
      ['51 - 80', 'Lv 380', '5 Soul + 1 Bless + 1 Creation', '800 point/lần'],
      ['81 - 100', 'Lv 400', '10 Soul + 2 Bless + 2 Creation', '1000 point/lần'],
      ['101 - 120', 'Lv 400', '15 Soul + 5 Bless + 5 Creation', '1200 point/lần']
    ]
  },
  {
    id: 'ingame-events',
    title: 'Sự kiện ingame',
    subtitle: 'Lịch cố định',
    icon: 'event',
    type: 'table',
    headers: ['Sự kiện', 'Thời gian', 'Map', 'Phần thưởng'],
    rows: [
      ['Blood Castle', '2h/lần', 'BC 1-7', 'Wing 2.5 Fragment + Jewel box'],
      ['Devil Square', '3h/lần', 'DS 1-7', 'Chaos Weapon + Kinh nghiệm'],
      ['Crywolf', '21:00 Thứ 7', 'Crywolf', 'Bless of Wolf + Jewel random'],
      ['Battle Soccer', '20:00 hằng ngày', 'Arena', 'Điểm danh vọng Guild'],
      ['Chaos Castle', '4h/lần', 'CC 1-6', 'Charming Gem + nguyên liệu Wing 3']
    ]
  },
  {
    id: 'roadmap',
    title: 'Lộ trình máy chủ',
    subtitle: 'Theo ngày open',
    icon: 'timeline',
    type: 'timeline',
    steps: [
      { day: 'Ngày 1 - 3', detail: 'Alpha Test + mở Blood Castle, Devil Square, PK tự do.' },
      { day: 'Ngày 4 - 7', detail: 'Open Beta chính thức, mở Crywolf và hệ thống Guild Alliance.' },
      { day: 'Ngày 8 - 14', detail: 'Khai mở Kanturu Event, Đua Top Reset chính thức.' },
      { day: 'Sau 14 ngày', detail: 'Unlock Castle Siege + sự kiện săn Boss Hoàng Kim nâng cao.' }
    ]
  },
  {
    id: 'guides',
    title: 'Hướng dẫn server',
    subtitle: 'Video & hình ảnh',
    icon: 'guide',
    type: 'guides',
    guides: [
      {
        title: 'Thiết lập Auto Buff chuẩn',
        description: 'Video hướng dẫn thiết lập macro buff chuẩn bị cho PK guild.',
        mediaType: 'video',
        media: 'https://www.youtube.com/embed/dQw4w9WgXcQ'
      },
      {
        title: 'Đường đi săn đồ thiên sứ',
        description: 'Ảnh minh hoạ tuyến đường farm đồ tại Lost Tower và Atlans.',
        mediaType: 'image',
        media: 'https://via.placeholder.com/640x360?text=Lo%20trinh%20farm%20do'
      },
      {
        title: 'Tối ưu điểm reset',
        description: 'Video giải thích build point cho từng class ở từng giai đoạn reset.',
        mediaType: 'video',
        media: 'https://www.youtube.com/embed/oHg5SJYRHA0'
      }
    ]
  },
  {
    id: 'donate',
    title: 'Donate server',
    subtitle: 'Ủng hộ duy trì',
    icon: 'donate',
    type: 'donate',
    account: {
      owner: 'Nguyễn Thành Công',
      bank: 'MB Bank',
      number: '06123456789',
      note: 'Noi dung: Musaigon + Tên nhân vật'
    },
    qr: 'https://via.placeholder.com/220x220.png?text=QR+Donate',
    reminder: 'Sau khi chuyển khoản, gửi biên lai cho Admin để được hỗ trợ nhanh nhất.'
  }
];

const announcements = [
  {
    title: 'Bảo trì định kỳ 28/11',
    date: '28.11.2025',
    content: 'Máy chủ sẽ bảo trì lúc 12h00 để cập nhật hệ thống chống gian lận mới. Ước tính downtime 20 phút.',
    media: { type: 'image', src: 'https://via.placeholder.com/400x200?text=Maintenance' }
  },
  {
    title: 'Ra mắt map Crywolf',
    date: '27.11.2025',
    content: 'Crywolf chính thức mở với phần thưởng Bless of Wolf dành cho tổ đội phòng thủ thành công.',
    media: { type: 'video', src: 'https://www.youtube.com/embed/aqz-KE-bpKQ' }
  },
  {
    title: 'Alpha Test kết thúc',
    date: '26.11.2025',
    content: 'Cảm ơn các chiến binh đã tham gia test. Tất cả dữ liệu sẽ được reset trước ngày Open Beta.'
  },
  {
    title: 'Thông báo Đua Top Reset',
    date: '25.11.2025',
    content: 'Đua Top Reset diễn ra từ ngày Open đến hết ngày thứ 14 với tổng giải thưởng 10 triệu VND.'
  },
  {
    title: 'Update Drop Ngọc',
    date: '24.11.2025',
    content: 'Tăng tỷ lệ rơi Jewel of Creation ở Kanturu và Raklion thêm 5% giúp người chơi ghép Wing nhanh hơn.'
  },
  {
    title: 'Event săn Boss Hoàng Kim',
    date: '23.11.2025',
    content: 'Boss hoàng kim xuất hiện tại Devias, Lost Tower, Atlans mỗi 4 tiếng. Drop nguyên liệu Wing 3.'
  },
  {
    title: 'Cập nhật Launcher',
    date: '22.11.2025',
    content: 'Launcher mới thêm tùy chọn auto update và tối ưu hiệu năng cho máy cấu hình yếu.',
    media: { type: 'image', src: 'https://via.placeholder.com/400x220?text=Launcher+Update' }
  }
];

const leaderboards = [
  {
    title: 'Đua Top Reset S1',
    short: 'Tính đến hết ngày thứ 14',
    description: 'Top reset sẽ nhận Jewel Box + tiền mặt chuyển khoản. Reset được tính tự động mỗi 5 phút.',
    rewards: ['Top 1: 3.000.000đ + Wing 3 chọn', 'Top 2: 2.000.000đ + 3 set luck option', 'Top 3: 1.000.000đ + 2 weapon ancient']
  },
  {
    title: 'Đua Top PK Guild',
    short: 'Arena 20h - 22h hằng ngày',
    description: 'Guild đạt số điểm PK cao nhất trong khung giờ sẽ nhận danh hiệu + quà tặng hiện vật.',
    rewards: ['Guild vô địch: 2.000.000đ + 200k Wcoin', 'Guild Á quân: 1.000.000đ + 150k Wcoin']
  },
  {
    title: 'Đua Top Master Level',
    short: 'Tính đến khi mở Kanturu',
    description: 'Trao thưởng cho chiến binh đạt Master Level sớm nhất với build điểm hợp lệ.',
    rewards: ['Top 1: Khiên socket độc quyền', 'Top 2: Vũ khí Ancient +15', 'Top 3: 100k Wcoin']
  }
];

const activityMedia = [
  {
    title: 'Castle Siege thử nghiệm',
    description: 'Gần 200 chiến binh tham gia test phòng thủ Loren.',
    type: 'video',
    embed: 'https://www.youtube.com/embed/VYOjWnS4cMY'
  },
  {
    title: 'Party săn boss đêm',
    description: 'Anh em tập trung tại Kanturu kiếm ngọc Creation.',
    type: 'image',
    src: 'https://via.placeholder.com/640x360?text=Party+Boss'
  },
  {
    title: 'Khoảnh khắc BK',
    description: 'Blade Knight solo 1v5 và vẫn sống sót.',
    type: 'video',
    embed: 'https://www.youtube.com/embed/tgbNymZ7vqY'
  }
];

const nostalgiaMedia = [
  {
    title: 'Ảnh chụp Devias 2008',
    description: 'Không khí tấp nập quen thuộc.',
    type: 'image',
    src: 'https://via.placeholder.com/640x360?text=Devias+2008'
  },
  {
    title: 'Video săn Kundun',
    description: 'Những trận PK tranh boss máu lửa.',
    type: 'video',
    embed: 'https://www.youtube.com/embed/ysz5S6PUM-U'
  },
  {
    title: 'Ảnh guild huyền thoại',
    description: 'Đội hình full set thiên sứ ngày trở lại.',
    type: 'image',
    src: 'https://via.placeholder.com/640x360?text=Guild+Legacy'
  }
];

const ANNOUNCEMENT_PAGE_SIZE = 5;
let currentAnnouncementPage = 1;

const init = () => {
  initGameMenu();
  initAnnouncements();
  initLeaderboards();
  initMediaSection('activity', activityMedia);
  initMediaSection('nostalgia', nostalgiaMedia);
  initCarousels();
  initModals();
  initFloatingPanels();
};

function initGameMenu() {
  const menu = document.getElementById('gameMenu');
  const panel = document.getElementById('gamePanelContent');
  if (!menu || !panel) return;
  menu.innerHTML = '';
  gameSections.forEach((section, index) => {
    const button = document.createElement('button');
    button.innerHTML = `${icons[section.icon] || ''}<div><strong>${section.title}</strong><small>${section.subtitle || ''}</small></div>`;
    if (index === 0) {
      button.classList.add('active');
      renderGameSection(section.id);
    }
    button.addEventListener('click', () => {
      menu.querySelectorAll('button').forEach((btn) => btn.classList.remove('active'));
      button.classList.add('active');
      renderGameSection(section.id);
    });
    menu.appendChild(button);
  });
}

function renderGameSection(id) {
  const section = gameSections.find((item) => item.id === id);
  const panel = document.getElementById('gamePanelContent');
  if (!section || !panel) return;
  panel.classList.remove('game-panel-placeholder');
  panel.classList.add('game-panel');
  panel.innerHTML = buildSectionMarkup(section);
}

function buildSectionMarkup(section) {
  let html = `<h4>${section.title}</h4>`;
  if (section.description) {
    html += `<p>${section.description}</p>`;
  }
  switch (section.type) {
    case 'stats':
      html += '<div class="stat-grid">';
      html += section.stats
        .map((stat) => `<div class="stat-card"><span>${stat.label}</span><strong>${stat.value}</strong></div>`)
        .join('');
      html += '</div>';
      if (section.note) {
        html += `<p class="helper-text">${section.note}</p>`;
      }
      break;
    case 'table':
      html += '<table><thead><tr>';
      html += section.headers.map((header) => `<th>${header}</th>`).join('');
      html += '</tr></thead><tbody>';
      html += section.rows.map((row) => `<tr>${row.map((cell) => `<td>${cell}</td>`).join('')}</tr>`).join('');
      html += '</tbody></table>';
      break;
    case 'timeline':
      html += '<div class="timeline">';
      html += section.steps
        .map((step) => `<div class="timeline-item"><strong>${step.day}</strong><p>${step.detail}</p></div>`)
        .join('');
      html += '</div>';
      break;
    case 'guides':
      html += section.guides
        .map(
          (guide) => `
            <details class="guide-card">
              <summary>${guide.title}</summary>
              <p>${guide.description}</p>
              ${renderGuideMedia(guide)}
            </details>
          `
        )
        .join('');
      break;
    case 'donate':
      html += `
        <div class="donate-card">
          <img src="${section.qr}" alt="Mã QR donate" />
          <p><strong>Chủ tài khoản:</strong> ${section.account.owner}</p>
          <p><strong>Ngân hàng:</strong> ${section.account.bank}</p>
          <p><strong>Số tài khoản:</strong> ${section.account.number}</p>
          <p><strong>Nội dung:</strong> ${section.account.note}</p>
          <p class="helper-text">${section.reminder}</p>
        </div>
      `;
      break;
    default:
      html += '<p>Đang cập nhật nội dung...</p>';
  }
  return html;
}

function renderGuideMedia(guide) {
  if (guide.mediaType === 'video') {
    return `<div class="ratio"><iframe src="${guide.media}" title="${guide.title}" allowfullscreen loading="lazy"></iframe></div>`;
  }
  if (guide.mediaType === 'image') {
    return `<img src="${guide.media}" alt="${guide.title}" loading="lazy" />`;
  }
  return '';
}

function initAnnouncements() {
  const prevBtn = document.getElementById('prevAnnouncements');
  const nextBtn = document.getElementById('nextAnnouncements');
  prevBtn?.addEventListener('click', () => changeAnnouncementPage(-1));
  nextBtn?.addEventListener('click', () => changeAnnouncementPage(1));
  renderAnnouncements();
}

function changeAnnouncementPage(delta) {
  const totalPages = Math.ceil(announcements.length / ANNOUNCEMENT_PAGE_SIZE);
  const nextPage = currentAnnouncementPage + delta;
  if (nextPage < 1 || nextPage > totalPages) return;
  currentAnnouncementPage = nextPage;
  renderAnnouncements();
}

function renderAnnouncements() {
  const list = document.getElementById('announcementList');
  const indicator = document.getElementById('announcementPage');
  if (!list || !indicator) return;
  const start = (currentAnnouncementPage - 1) * ANNOUNCEMENT_PAGE_SIZE;
  const pageItems = announcements.slice(start, start + ANNOUNCEMENT_PAGE_SIZE);
  list.innerHTML = '';
  pageItems.forEach((item) => {
    const card = document.createElement('article');
    card.className = 'card';
    if (item.media) {
      card.appendChild(createMediaElement(item.media));
    }
    const header = document.createElement('div');
    header.className = 'card-header';
    header.innerHTML = `<h3>${item.title}</h3><time>${item.date}</time>`;
    const content = document.createElement('p');
    content.textContent = item.content;
    card.appendChild(header);
    card.appendChild(content);
    list.appendChild(card);
  });
  const totalPages = Math.ceil(announcements.length / ANNOUNCEMENT_PAGE_SIZE) || 1;
  indicator.textContent = `${currentAnnouncementPage}/${totalPages}`;
}

function createMediaElement(media) {
  if (media.type === 'video') {
    const frame = document.createElement('iframe');
    frame.src = media.src;
    frame.title = 'Media video';
    frame.loading = 'lazy';
    frame.setAttribute('allowfullscreen', 'true');
    return frame;
  }
  const img = document.createElement('img');
  img.src = media.src;
  img.alt = 'Ảnh thông báo';
  img.loading = 'lazy';
  return img;
}

function initLeaderboards() {
  const container = document.getElementById('leaderboardList');
  if (!container) return;
  container.innerHTML = '';
  leaderboards.forEach((item) => {
    const card = document.createElement('article');
    card.className = 'leaderboard-card';
    card.innerHTML = `<h3>${item.title}</h3><p>${item.short}</p>`;
    card.addEventListener('click', () => openLeaderboardModal(item));
    container.appendChild(card);
  });
}

function openLeaderboardModal(item) {
  const modal = document.getElementById('leaderboardModal');
  if (!modal) return;
  modal.querySelector('#leaderboardModalTitle').textContent = item.title;
  modal.querySelector('#leaderboardModalDesc').textContent = item.description;
  const list = modal.querySelector('#leaderboardModalRewards');
  list.innerHTML = item.rewards.map((reward) => `<li>${reward}</li>`).join('');
  openModal('leaderboardModal');
}

function initMediaSection(name, items) {
  const track = document.querySelector(`[data-carousel-track="${name}"]`);
  if (!track) return;
  track.innerHTML = '';
  items.forEach((item) => {
    const card = document.createElement('article');
    card.className = 'media-card';
    let mediaMarkup = '';
    if (item.type === 'video') {
      mediaMarkup = `<iframe src="${item.embed}" title="${item.title}" loading="lazy" allowfullscreen></iframe>`;
    } else {
      mediaMarkup = `<img src="${item.src}" alt="${item.title}" loading="lazy" />`;
    }
    card.innerHTML = `${mediaMarkup}<h4>${item.title}</h4><p>${item.description}</p>`;
    track.appendChild(card);
  });
}

function initCarousels() {
  document.querySelectorAll('[data-carousel-control]').forEach((button) => {
    button.addEventListener('click', () => {
      const track = document.querySelector(`[data-carousel-track="${button.dataset.target}"]`);
      if (!track) return;
      const direction = button.dataset.direction === 'next' ? 1 : -1;
      track.scrollBy({ left: direction * track.clientWidth * 0.8, behavior: 'smooth' });
    });
  });
  ['activity', 'nostalgia'].forEach((name) => {
    const track = document.querySelector(`[data-carousel-track="${name}"]`);
    if (!track) return;
    setInterval(() => autoScrollTrack(track), 7000);
  });
}

function autoScrollTrack(track) {
  if (!track) return;
  const atEnd = track.scrollLeft + track.clientWidth >= track.scrollWidth - 5;
  if (atEnd) {
    track.scrollTo({ left: 0, behavior: 'smooth' });
  } else {
    track.scrollBy({ left: track.clientWidth * 0.8, behavior: 'smooth' });
  }
}

function initModals() {
  document.querySelectorAll('[data-open-modal]').forEach((btn) => {
    btn.addEventListener('click', () => openModal(btn.dataset.openModal));
  });
  document.querySelectorAll('[data-close-modal]').forEach((btn) => {
    btn.addEventListener('click', () => closeModal(btn.closest('.modal')));
  });
  document.querySelectorAll('.modal').forEach((modal) => {
    modal.addEventListener('click', (event) => {
      if (event.target === modal) {
        closeModal(modal);
      }
    });
  });
}

function openModal(id) {
  const modal = document.getElementById(id);
  if (!modal) return;
  modal.classList.add('active');
  document.body.style.overflow = 'hidden';
}

function closeModal(modal) {
  if (!modal) return;
  modal.classList.remove('active');
  if (!document.querySelector('.modal.active')) {
    document.body.style.overflow = '';
  }
}

function initFloatingPanels() {
  const panels = document.querySelectorAll('[data-follow-cursor]');
  if (!panels.length) return;
  const mediaQuery = window.matchMedia('(max-width: 1200px)');
  if (mediaQuery.matches) return;
  window.addEventListener('pointermove', (event) => {
    const x = (event.clientX / window.innerWidth - 0.5) * 40;
    const y = (event.clientY / window.innerHeight - 0.5) * 30;
    panels.forEach((panel) => {
      const speed = parseFloat(panel.dataset.speed || 0.02);
      panel.style.transform = `translate(${x * speed}px, ${y * speed}px)`;
    });
  });
}

document.addEventListener('DOMContentLoaded', init);
