
<script>
    document.addEventListener('DOMContentLoaded', () => {
        const form = document.getElementById('booking-form');
    const submitButton = document.querySelector('.submit');

    submitButton.addEventListener('click', function (e) {
        e.preventDefault(); // Form göndərilməsin

    Swal.fire({
        icon: 'success',
    title: 'Rezervasiya tamamlandı!',
    text: 'Rezervasiyanız uğurla yerinə yetirildi.',
    confirmButtonText: 'OK'
            }).then(() => {
        form.reset(); // formu sıfırla
    document.querySelector('.tableForm').innerHTML = `<option disabled selected value="">Table</option>`;
    document.querySelector('.errorMessage').textContent = '';
                form.querySelectorAll('.text-danger').forEach(el => el.textContent = '');
            });
        });
    });
</script>
