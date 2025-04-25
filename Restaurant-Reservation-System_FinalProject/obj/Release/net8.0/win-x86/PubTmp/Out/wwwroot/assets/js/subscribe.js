document.getElementById("subscribeForm").addEventListener("submit", function (e) {
    e.preventDefault(); // Sayfanın yeniden yüklenmesini engelle

    const emailInput = this.querySelector('input[name="Email"]');
    const email = emailInput.value.trim();

    // Basit e-posta kontrolü (regex ile)
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email)) {
        Swal.fire({
            icon: 'warning',
            title: 'Geçersiz e-posta!',
            text: 'Lütfen geçerli bir e-posta adresi girin (örnek: example@mail.com).'
        });
        return; // Eğer geçersizse fetch() çalışmasın
    }

    const formData = new FormData(this);

    fetch("/Home/AddSubscriber", {
        method: "POST",
        body: formData
    })
        .then(response => {
            if (response.ok) {
                Swal.fire({
                    icon: 'success',
                    title: 'Abonelik Olundunuz!',
                    text: 'Teşekkür ederiz, artık bültenimize abonesiniz.'
                });
                this.reset(); // Formu temizle
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Xeta!',
                    text: 'Abonelik sırasında bir xeta yarandi.'
                });
            }
        })
        .catch(error => {
            console.error("Hata:", error);
            Swal.fire({
                icon: 'error',
                title: 'Bağlantı hatası!',
                text: 'Lütfen internet bağlantınızı kontrol edin.'
            });
        });
});

