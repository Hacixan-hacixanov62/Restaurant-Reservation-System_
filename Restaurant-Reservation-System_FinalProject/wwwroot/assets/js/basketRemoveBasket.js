document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".remove-from-cart").forEach(button => {
        button.addEventListener("click", async function (event) {
            event.preventDefault();

            const url = this.getAttribute("href"); // Silmə URL-si
            const row = this.closest("tr"); // Məhsulun tr hissəsi

            try {
                const response = await fetch(url, { method: "GET" });

                if (response.ok) {
                    // SweetAlert mesajı
                    Swal.fire({
                        icon: 'success',
                        title: 'Məhsul silindi!',
                        text: 'Məhsul səbətdən uğurla silindi.',
                        timer: 1500,
                        showConfirmButton: false
                    });

                    // HTML-dən sil
                    if (row) {
                        row.remove();
                    }

                    // Ümumi qiyməti yenilə
                    updateTotal();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Silmə alınmadı',
                        text: 'Məhsul səbətdən silinərkən xəta baş verdi.'
                    });
                }
            } catch (error) {
                console.error("Xəta:", error);
                Swal.fire({
                    icon: 'error',
                    title: 'Bağlantı Xətası!',
                    text: 'Serverə bağlanmaq mümkün olmadı.'
                });
            }
        });
    });

    // Ümumi toplamı yeniləyən funksiya
    function updateTotal() {
        let total = 0;
        document.querySelectorAll(".BasketItemTotalPrice").forEach(el => {
            let priceText = el.textContent.replace("$", "").replace("₼", "");
            let price = parseFloat(priceText);
            if (!isNaN(price)) {
                total += price;
            }
        });

        document.querySelectorAll(".woocommerce-Price-amount").forEach(el => {
            el.textContent = `$${total.toFixed(2)}`;
        });
    }
});