document.addEventListener("DOMContentLoaded", function () {
  var header = document.querySelector("header");
  var headerhome = document.querySelector(".header-home");
  var headerLinks = document.querySelectorAll("header ul li a");
  var headerLogo = document.querySelector('.header-logo a img');

  function updateHeader() {
    var scrollPosition = window.scrollY;
    var isAccountPage = window.location.pathname.includes("/account"); 

    if (scrollPosition > 40) {
      header.style.backgroundColor = "#000";
      header.style.height = "80px";
      headerhome.style.padding = "20px 15px";

      if (isAccountPage) {
        headerLinks.forEach(link => {
          link.style.color = "#fff";
        });
      }
    } else {
      headerhome.style.padding = "50px 15px 20px 15px";
      header.style.backgroundColor = "transparent";

      if (isAccountPage) {
        headerLinks.forEach(link => {
          link.style.color = "#000";
        });
      }
    }

    if (isAccountPage && scrollPosition === 0) {
      if (headerLogo) {
        headerLogo.style.filter = "invert(1)";
      }
    } else {
      if (headerLogo) {
        headerLogo.style.filter = "none";
      }
    }
  }

  updateHeader();

  window.addEventListener("scroll", updateHeader);
});
