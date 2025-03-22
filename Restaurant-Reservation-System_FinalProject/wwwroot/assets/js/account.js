const list_items = document.querySelectorAll('.item');
const order_section = document.querySelector('.OrderSection');
const account_section = document.querySelector('.AccountSection');
const dashboard_section = document.querySelector('.DashboardSection');

function changedItemsColor() {
    list_items.forEach(item => {
        item.addEventListener('click', () => {
            list_items.forEach(item => item.classList.remove('active'));
            item.classList.add('active');
        });
    });
}

changedItemsColor();

function showOtherSections() {
    list_items.forEach((item, index) => {
        item.addEventListener('click', () => {
            order_section.style.opacity  = 'none';
            account_section.style.display = 'none';
            dashboard_section.style.display = 'none';

            if (index === 0) {
                dashboard_section.style.display = 'block';
                order_section.style.display = 'none';
                account_section.style.display = 'none';

            } else if (index === 1) {
                order_section.style.display = 'block';
                dashboard_section.style.display = 'none';
                account_section.style.display = 'none';
            } else if (index === 2) {
                account_section.style.display = 'block';
                dashboard_section.style.display = 'none';
                order_section.style.display = 'none';
            }
        });
    });
}

showOtherSections();