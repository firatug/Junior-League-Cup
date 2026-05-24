(function () {
    'use strict';

    const header = document.getElementById('siteHeader');
    const navToggle = document.getElementById('navToggle');
    const mainNav = document.getElementById('mainNav');
    const loader = document.getElementById('pageLoader');

    window.addEventListener('load', () => {
        if (loader) {
            loader.classList.add('hidden');
            setTimeout(() => loader.remove(), 600);
        }
    });

    window.addEventListener('scroll', () => {
        if (!header) return;
        header.classList.toggle('scrolled', window.scrollY > 40);
    });

    if (navToggle && mainNav) {
        navToggle.addEventListener('click', () => {
            const open = mainNav.classList.toggle('open');
            navToggle.classList.toggle('active', open);
            navToggle.setAttribute('aria-expanded', open ? 'true' : 'false');
            document.body.classList.toggle('nav-open', open);
        });

        mainNav.querySelectorAll('a:not(.nav-dropdown-toggle)').forEach(link => {
            link.addEventListener('click', () => {
                mainNav.classList.remove('open');
                navToggle.classList.remove('active');
                navToggle.setAttribute('aria-expanded', 'false');
                document.body.classList.remove('nav-open');
            });
        });
    }

    const navDropdowns = document.querySelectorAll('[data-nav-dropdown]');
    navDropdowns.forEach(dropdown => {
        const toggle = dropdown.querySelector('.nav-dropdown-toggle');
        if (!toggle) return;

        toggle.addEventListener('click', (e) => {
            e.stopPropagation();
            const isOpen = dropdown.classList.contains('open');
            navDropdowns.forEach(d => {
                d.classList.remove('open');
                d.querySelector('.nav-dropdown-toggle')?.setAttribute('aria-expanded', 'false');
            });
            if (!isOpen) {
                dropdown.classList.add('open');
                toggle.setAttribute('aria-expanded', 'true');
            }
        });
    });

    document.addEventListener('click', () => {
        navDropdowns.forEach(dropdown => {
            dropdown.classList.remove('open');
            dropdown.querySelector('.nav-dropdown-toggle')?.setAttribute('aria-expanded', 'false');
        });
    });

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            navDropdowns.forEach(dropdown => {
                dropdown.classList.remove('open');
                dropdown.querySelector('.nav-dropdown-toggle')?.setAttribute('aria-expanded', 'false');
            });
        }
    });

    const counters = document.querySelectorAll('[data-count]');
    const animateCounter = (el) => {
        const target = parseInt(el.getAttribute('data-count'), 10);
        const suffix = el.getAttribute('data-suffix') || '';
        const duration = 1800;
        const start = performance.now();

        const step = (now) => {
            const progress = Math.min((now - start) / duration, 1);
            const eased = 1 - Math.pow(1 - progress, 3);
            el.textContent = Math.floor(target * eased) + suffix;
            if (progress < 1) requestAnimationFrame(step);
        };
        requestAnimationFrame(step);
    };

    if (counters.length && 'IntersectionObserver' in window) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting && !entry.target.dataset.animated) {
                    entry.target.dataset.animated = 'true';
                    animateCounter(entry.target);
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.4 });
        counters.forEach(c => observer.observe(c));
    }

    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', (e) => {
            const id = anchor.getAttribute('href');
            if (id.length <= 1) return;
            const target = document.querySelector(id);
            if (target) {
                e.preventDefault();
                const headerOffset = header ? header.offsetHeight + 20 : 100;
                const top = target.getBoundingClientRect().top + window.scrollY - headerOffset;
                window.scrollTo({ top, behavior: 'smooth' });
            }
        });
    });

    const timeline = document.getElementById('jlcTimeline');
    if (timeline && 'IntersectionObserver' in window) {
        const timelineObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                timeline.classList.toggle('is-active', entry.isIntersecting);
            });
        }, { threshold: 0.35 });
        timelineObserver.observe(timeline);
    }
})();
