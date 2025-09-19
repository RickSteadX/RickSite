// Theme management functions

// Check if user has a theme preference stored in localStorage
export function getThemePreference() {
    const savedTheme = localStorage.getItem('theme-preference');
    
    if (savedTheme) {
        return savedTheme === 'dark';
    } else {
        // Check if user's system prefers dark mode
        const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        return prefersDark;
    }
}

// Save user's theme preference to localStorage
export function setThemePreference(isDarkTheme) {
    localStorage.setItem('theme-preference', isDarkTheme ? 'dark' : 'light');
    return isDarkTheme;
}

// Initialize particles.js for the hero section
export function initParticles() {
    if (typeof particlesJS !== 'undefined' && document.getElementById('particles-js')) {
        particlesJS('particles-js', {
            particles: {
                number: {
                    value: 80,
                    density: {
                        enable: true,
                        value_area: 800
                    }
                },
                color: {
                    value: '#ffffff'
                },
                shape: {
                    type: 'circle',
                    stroke: {
                        width: 0,
                        color: '#000000'
                    },
                    polygon: {
                        nb_sides: 5
                    }
                },
                opacity: {
                    value: 0.5,
                    random: false,
                    anim: {
                        enable: false,
                        speed: 1,
                        opacity_min: 0.1,
                        sync: false
                    }
                },
                size: {
                    value: 3,
                    random: true,
                    anim: {
                        enable: false,
                        speed: 40,
                        size_min: 0.1,
                        sync: false
                    }
                },
                line_linked: {
                    enable: true,
                    distance: 150,
                    color: '#ffffff',
                    opacity: 0.4,
                    width: 1
                },
                move: {
                    enable: true,
                    speed: 2,
                    direction: 'none',
                    random: false,
                    straight: false,
                    out_mode: 'out',
                    bounce: false,
                    attract: {
                        enable: false,
                        rotateX: 600,
                        rotateY: 1200
                    }
                }
            },
            interactivity: {
                detect_on: 'canvas',
                events: {
                    onhover: {
                        enable: true,
                        mode: 'grab'
                    },
                    onclick: {
                        enable: true,
                        mode: 'push'
                    },
                    resize: true
                },
                modes: {
                    grab: {
                        distance: 140,
                        line_linked: {
                            opacity: 1
                        }
                    },
                    bubble: {
                        distance: 400,
                        size: 40,
                        duration: 2,
                        opacity: 8,
                        speed: 3
                    },
                    repulse: {
                        distance: 200,
                        duration: 0.4
                    },
                    push: {
                        particles_nb: 4
                    },
                    remove: {
                        particles_nb: 2
                    }
                }
            },
            retina_detect: true
        });
    }
}

// Initialize parallax scrolling effect
export function initParallax() {
    window.addEventListener('scroll', function() {
        const parallaxElements = document.querySelectorAll('.parallax-element');
        
        parallaxElements.forEach(element => {
            const position = element.getBoundingClientRect();
            const speed = element.dataset.speed || 0.5;
            
            // Only apply parallax if element is in viewport
            if (position.top < window.innerHeight && position.bottom > 0) {
                const yPos = -(window.scrollY * speed);
                element.style.transform = `translateY(${yPos}px)`;
            }
        });
    });
}

// Initialize smooth page transitions
export function initPageTransitions() {
    document.addEventListener('DOMContentLoaded', () => {
        document.body.classList.add('fade-in');
    });
}

// Initialize all JavaScript features
export function initAll() {
    initParticles();
    initParallax();
    initPageTransitions();
}