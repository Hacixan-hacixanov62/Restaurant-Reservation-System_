const switchButton = document.querySelector('.switch-button');
const fa_moon = document.querySelector('.fa-moon');
const light = document.querySelector('.light');

const savedMode = localStorage.getItem('mode');
if (savedMode === "dark") {
    fa_moon.classList.add('fa-sun');
    fa_moon.classList.remove('fa-moon');
    
    light.classList.add('dark');
    light.classList.remove('light');
}

function mode() {
    switchButton.addEventListener('click', () => {
        if (fa_moon.classList.contains('fa-moon')) {
            fa_moon.classList.add('fa-sun');
            fa_moon.classList.remove('fa-moon');

            light.classList.add('dark');
            light.classList.remove('light');

            localStorage.setItem("mode", "dark");
        } else {
            // Light mode aktiv edilir
            fa_moon.classList.add('fa-moon');
            fa_moon.classList.remove('fa-sun');

            light.classList.add('light');
            light.classList.remove('dark');

            localStorage.setItem("mode", "light");
        }
    });
}

function sendModeToBackend(mode) {
    fetch('/Theme/SetTheme', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ theme: mode })
    });
}

mode();

localStorage.setItem("mode", "dark");
sendModeToBackend("dark");

localStorage.setItem("mode", "light");
sendModeToBackend("light");

