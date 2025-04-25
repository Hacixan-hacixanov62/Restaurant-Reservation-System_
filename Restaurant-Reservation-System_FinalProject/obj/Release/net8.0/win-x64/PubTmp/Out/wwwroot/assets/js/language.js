const language_button = document.querySelector('.language-button');
const languageItems = document.querySelectorAll('[data-language]');
const span = document.querySelector('.language-button span');

const translations = {
    en: {
        // Navigation
        'nav.home': 'Home',
        'nav.menu': 'Menu',
        'nav.reservation': 'Reservation',
        'nav.blog': 'Blog',
        'nav.shop': 'Shop',
        'nav.contact': 'Contact',
        
        // Header
        'header.login': 'LogIn',
        'header.register': 'Register',
        'header.items': 'ITEM(S)',
        'header.total': 'Total',
        
        // Restaurant Section
        'restaurant.welcome': 'Welcome at',
        'restaurant.name': "danny's restaurant",
        'restaurant.aboutUs': 'about us',
        
        // Menu Section
        'menu.title': 'Restaurant',
        'menu.subtitle': 'menu',
        'menu.addToCart': 'Add to cart',
        'menu.next': 'NEXT',
        'menu.whatsOnMenu': "what's on the menu",
        'menu.viewAll': 'view all',
        
        // Categories
        'category.all': 'all',
        'category.starters': 'starters',
        'category.main': 'main',
        'category.dessert': 'DESSERT',
        'category.drinks': 'drinks',
        'category.dailyMenu': 'daily menu',
        'category.specialty': "chef's specialty",
        'category.offers': 'offers',
        
        // Specialties Section
        'specialties.chinese': 'Chinese',
        'specialties.title': 'specialties',
        'specialties.todaySpecial': "today's specialty",
        'specialties.creme': 'La creme de la creme',
        'specialties.roastedDuck': 'Oven Roasted duck with special sousage',
        'specialties.readMore': 'read more',
        
        // Menu List
        'menuList.restaurant': 'restaurant',
        'menuList.menu': 'menu',
        'menuList.all': 'all',
        'menuList.starters': 'starters',
        'menuList.main': 'main',
        'menuList.dessert': 'DESSERT',
        'menuList.drinks': 'drinks',
        'menuList.dailyMenu': 'daily menu',
        'menuList.chefSpecialty': "chef's specialty",
        'menuList.offers': 'offers',
        'menuList.addToCart': 'Add to cart',
        'menuList.next': 'NEXT',
        
        // Testimonials
        'testimonials.clients': 'Clients',
        'testimonials.title': 'testimonials',
        'testimonials.author': 'james de franco',
        
        // Reservation
        'reservation.title': 'reservations',
        'reservation.address': '49 Featherstone Street LONDON EC1Y 8SY UNITED KINGDOM',
        'reservation.phone': '+4 1800 555 1234',
        'reservation.email': 'bookatable@restaurant.com',
        'reservation.online': 'ONLINE',
        
        // Subscribe
        'subscribe.discount': '15% OFF',
        'subscribe.title': 'Subscribe to our',
        'subscribe.newsletter': 'newsletter',
        'subscribe.description': 'Subscribe to our newsletter and receive 15% discount from your order.',
        'subscribe.placeholder': 'Your@mail.com',
        'subscribe.button': 'subscribe',
        
        // Footer
        'footer.subscribe': 'Subscribe to our',
        'footer.newsletter': 'newsletter',
        'footer.discount': 'Subscribe to our newsletter and receive 15% discount from your order.',
        'footer.chefInfo': 'Chef Taylor Bonnyman, working in collaboration with Head Chef Marguerite Keogh, offer elegant & playful modern British cooking.',
        'footer.follow': 'follow',
        'footer.onlineReservation': 'online reservation',
        'footer.copyright': 'copyright © 2016',
        'footer.rights': 'All rights reserved.',
        
        // Blog Page
        'blog.restaurant': 'Restaurant',
        'blog.blog': 'Blog',
        'blog.subtitle': 'We Cook & Blog to your liking',
        'blog.restaurant_name': "Danny's",
        'blog.categories': 'Categories',
        'blog.all': 'All',
        'blog.chef_specialty': "CHEF'S SPECIALITY",
        'blog.drinks': 'DRINKS',
        'blog.desert': 'DESERT',
        'blog.main': 'MAIN',
        'blog.offers': 'OFFERS',
        'blog.starters': 'STARTERS',
        'blog.daily_menu': 'DAILY MENU',
        'blog.follow_us': 'Follow Us',
        'blog.newsletter_title': 'NEWSLETTER',
        'blog.newsletter_subtitle': 'Subscribe & receive 15% discount coupon',
        'blog.featured': 'FEATURED',
        'blog.under': 'under',
        
        // Blog Page Additional
        'blog.special_deal': "Special deal if you order 3 or more pizza's",
        'blog.posted_date': 'JUNE 15, 2016',
        'blog.posted_by': 'by',
        'blog.author': 'MARIUS H',
        'blog.in_category': 'in',
        'blog.category_photo': 'PHOTOGRAPHY',
        'blog.lorem': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
        'blog.subscribe_btn': 'SUBSCRIBE',
        'blog.email_placeholder': 'your.address@email.com',
        
        // Footer Additional
        'footer.address.phone': '+4 1800 555 1234',
        'footer.address.email': 'bookatable@restaurant.com',
        'footer.address.street': '49 Featherstone Street',
        'footer.address.city': 'LONDON',
        'footer.address.postal': 'EC1Y 8SY',
        'footer.address.country': 'UNITED KINGDOM',
        'footer.demo_text': 'This Demo is also part of',
        'footer.theme_name': 'Kallyas WordPress Theme',
        'footer.company': 'Hogash.com',
        
        // Menu Page
        'menu.banner.restaurant': 'Restaurant',
        'menu.banner.menu': 'Menu',
        'menu.banner.juicy': 'Juicy Flavours',
        'menu.asparagus': 'Asparagus & Halloumi Salad',
        'menu.weight': '(250 G)',
        'menu.ingredients': 'Beans / Halloumi cheese / Asparagus / Courgettes / Cherry tomatoes',
        'menu.tasty': 'Tasty',
        'menu.default_sorting': 'Default sorting',
        'menu.sort_popularity': 'Sort by popularity',
        'menu.sort_rating': 'Sort by average rating',
        'menu.sort_price_low': 'Sort by price: low to high',
        'menu.sort_price_high': 'Sort by price: high to low',
        
        // Reservation Page
        'reservation.banner.make': 'make an online',
        'reservation.banner.title': 'Reservation',
        'reservation.banner.call': 'Or call us at',
        'reservation.banner.time': '10:00 AM - 07:00 PM',
        'reservation.form.title': 'Booking place for your dinner!',
        'reservation.form.name': 'Your name',
        'reservation.form.phone': 'Your phone number',
        'reservation.form.date': 'Date',
        'reservation.form.time': 'Time',
        'reservation.form.guests': 'Number of guests',
        'reservation.form.submit': 'BOOK NOW',
        
        // Shop Page
        'shop.banner.restaurant': 'Restaurant',
        'shop.banner.shop': 'Shop',
        'shop.title': 'Shop',
        'shop.showing': 'SHOWING 1–11 OF 14 RESULTS',
        'shop.next': 'NEXT',
        
        // Contact Page
        'contact.banner.restaurant': 'Restaurant',
        'contact.banner.contact': 'Contact',
        'contact.banner.juicy': 'juicy flavours',
        'contact.details': 'contact details',
        'contact.form.name': 'name',
        'contact.form.email': 'Email',
        'contact.form.message': 'Message',
        'contact.form.submit': 'Send Message',

        // Home Page
        'home.banner.welcome': 'Welcome at',
        'home.banner.restaurant': 'restaurant',
        'home.banner.name': "danny's",
        'home.banner.aboutUs': 'about us',
        'home.banner.readMore': 'read more',
        
        // Menu Section
        'home.menu.restaurant': 'Restaurant',
        'home.menu.menu': 'menu',
        'home.menu.whatsOn': "what's on the menu",
        'home.menu.whatsOnMenu': "what's on the menu",
        'home.menu.viewAll': 'view all',
        'home.menu.addToCart': 'Add to cart',
        
        // Specialties Section
        'home.specialties.chinese': 'Chinese',
        'home.specialties.cuisine': 'cuisine',
        'home.specialties.title': 'specialties',
        'home.specialties.todaySpecial': "today's specialty",
        'home.specialties.creme': 'La creme de la creme',
        'home.specialties.roastedDuck': 'Oven Roasted duck with special sousage',
        'home.specialties.readMore': 'read more',
        'home.specialties.description': 'Our dishes are made with fresh ingredients of the highest quality',
        
        // Testimonials Section
        'home.testimonials.clients': 'Clients',
        'home.testimonials.testimonials': 'testimonials',
        'home.testimonials.author': 'james de franco',
        'home.testimonials.text': 'Chef Taylor Bonnyman, working in collaboration with Head Chef Marguerite Keogh, offer elegant & playful modern British cooking.',

        // Reservation Section
        'home.reservation.title': 'reservations',
        'home.reservation.address': '49 Featherstone Street LONDON EC1Y 8SY UNITED KINGDOM',
        'home.reservation.phone': '+4 1800 555 1234',
        'home.reservation.email': 'bookatable@restaurant.com',
        'home.reservation.online': 'ONLINE',

        // Subscribe Section
        'home.subscribe.discount': '15% OFF',
        'home.subscribe.title': 'Subscribe to our',
        'home.subscribe.newsletter': 'newsletter',
        'home.subscribe.description': 'Subscribe to our newsletter and receive 15% discount from your order',
        'home.subscribe.placeholder': 'Your@mail.com',
        'home.subscribe.button': 'subscribe',

        // Categories
        'home.category.all': 'all',
        'home.category.starters': 'starters',
        'home.category.main': 'main',
        'home.category.dessert': 'DESSERT',
        'home.category.drinks': 'drinks',
        'home.category.dailyMenu': 'daily menu',
        'home.category.specialty': "chef's specialty",
        'home.category.offers': 'offers',

        // Menu Items
        'home.menu.item1.title': 'Sałat z ogórków i szynki',
        'home.menu.item1.weight': '(250 G)',
        'home.menu.item1.ingredients': 'Fasola / Szynka / Ogórek / Cukinia / Pomidory cherry',
        'home.menu.item1.price': '22.00 ₽',

        'home.menu.item2.title': 'Krewetki z czosnkiem',
        'home.menu.item2.weight': '(200 G)',
        'home.menu.item2.ingredients': 'Krewetki / Czosnek / Olej / Limon / Zielenina',
        'home.menu.item2.price': '35.00 ₽',
    },
    az: {
        // Navigation
        'nav.home': 'Ana Səhifə',
        'nav.menu': 'Menyu',
        'nav.reservation': 'Rezervasiya',
        'nav.blog': 'Bloq',
        'nav.shop': 'Mağaza',
        'nav.contact': 'Əlaqə',
        
        // Header
        'header.login': 'Giriş',
        'header.register': 'Qeydiyyat',
        'header.items': 'MƏHSUL(LAR)',
        'header.total': 'Cəmi',
        
        // Restaurant Section
        'restaurant.welcome': 'Xoş gəlmisiniz',
        'restaurant.name': "danny's restoranı",
        'restaurant.aboutUs': 'haqqımızda',
        
        // Menu Section
        'menu.title': 'Restoran',
        'menu.subtitle': 'menyu',
        'menu.addToCart': 'Səbətə əlavə et',
        'menu.next': 'NÖVBƏTİ',
        'menu.whatsOnMenu': 'menyuda nə var',
        'menu.viewAll': 'hamısına bax',
        
        // Categories
        'category.all': 'hamısı',
        'category.starters': 'başlanğıclar',
        'category.main': 'əsas',
        'category.dessert': 'DESERT',
        'category.drinks': 'içkilər',
        'category.dailyMenu': 'günün menyusu',
        'category.specialty': 'şef təklifi',
        'category.offers': 'təkliflər',
        
        // Specialties Section
        'specialties.chinese': 'Çin',
        'specialties.title': 'təamlar',
        'specialties.todaySpecial': 'günün təklifi',
        'specialties.creme': 'Ən yaxşıların ən yaxşısı',
        'specialties.roastedDuck': 'Xüsusi soslu sobada bişmiş ördək',
        'specialties.readMore': 'ətraflı',
        
        // Menu List
        'menuList.restaurant': 'restoran',
        'menuList.menu': 'menyu',
        'menuList.all': 'hamısı',
        'menuList.starters': 'başlanğıclar',
        'menuList.main': 'əsas',
        'menuList.dessert': 'DESERT',
        'menuList.drinks': 'içkilər',
        'menuList.dailyMenu': 'günün menyusu',
        'menuList.chefSpecialty': 'şef təklifi',
        'menuList.offers': 'təkliflər',
        'menuList.addToCart': 'Səbətə əlavə et',
        'menuList.next': 'NÖVBƏTİ',
        
        // Testimonials
        'testimonials.clients': 'Müştərilər',
        'testimonials.title': 'rəylər',
        'testimonials.author': 'james de franko',
        
        // Reservation
        'reservation.title': 'rezervasiyalar',
        'reservation.address': '49 Featherstone Street LONDON EC1Y 8SY BİRLƏŞMİŞ KRALLIĞ',
        'reservation.phone': '+4 1800 555 1234',
        'reservation.email': 'bookatable@restaurant.com',
        'reservation.online': 'ONLİNE',
        
        // Subscribe
        'subscribe.discount': '15% ENDİRİM',
        'subscribe.title': 'Yeniliklərimizə abunə olun',
        'subscribe.newsletter': 'bülleten',
        'subscribe.description': 'Yeniliklərimizə abunə olun və sifarişinizdən 15% endirim əldə edin.',
        'subscribe.placeholder': 'Sizin@poçt.com',
        'subscribe.button': 'abunə ol',
        
        // Footer
        'footer.subscribe': 'Yeniliklərimizə abunə olun',
        'footer.newsletter': 'bülleten',
        'footer.discount': 'Bülleteninimizə abunə olun və sifarişinizdən 15% endirim əldə edin.',
        'footer.chefInfo': 'Şef Taylor Bonnyman, Baş Aşbaz Marguerite Keogh ilə əməkdaşlıq edərək, zərif və əyləncəli müasir Britaniya mətbəxi təklif edir.',
        'footer.follow': 'izlə',
        'footer.onlineReservation': 'online rezervasiya',
        'footer.copyright': 'müəllif hüququ © 2016',
        'footer.rights': 'Bütün hüquqlar qorunur.',
        
        // Blog Page
        'blog.restaurant': 'Restoran',
        'blog.blog': 'Bloq',
        'blog.subtitle': 'Sizin zövqünüzə uyğun bişirir və yazırıq',
        'blog.restaurant_name': "Danny's",
        'blog.categories': 'Kateqoriyalar',
        'blog.all': 'Hamısı',
        'blog.chef_specialty': 'ŞEF TƏKLİFİ',
        'blog.drinks': 'İÇKİLƏR',
        'blog.desert': 'DESERT',
        'blog.main': 'ƏSAS',
        'blog.offers': 'TƏKLİFLƏR',
        'blog.starters': 'BAŞLANĞICLAR',
        'blog.daily_menu': 'GÜNÜN MENYUSU',
        'blog.follow_us': 'Bizi İzləyin',
        'blog.newsletter_title': 'BÜLLETEN',
        'blog.newsletter_subtitle': 'Abunə olun və 15% endirim kuponu əldə edin',
        'blog.featured': 'SEÇİLMİŞLƏR',
        'blog.under': 'kateqoriyalar',
        
        // Blog Page Additional
        'blog.special_deal': "3 və ya daha çox pizza sifariş etdikdə xüsusi təklif",
        'blog.posted_date': '15 İYUN, 2016',
        'blog.posted_by': 'müəllif',
        'blog.author': 'MARİUS H',
        'blog.in_category': 'kateqoriya',
        'blog.category_photo': 'FOTOQRAFİYA',
        'blog.lorem': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
        'blog.subscribe_btn': 'ABUNƏ OL',
        'blog.email_placeholder': 'sizin.email@poct.com',
        
        // Footer Additional
        'footer.address.phone': '+4 1800 555 1234',
        'footer.address.email': 'bookatable@restaurant.com',
        'footer.address.street': '49 Featherstone Küçəsi',
        'footer.address.city': 'LONDON',
        'footer.address.postal': 'EC1Y 8SY',
        'footer.address.country': 'BİRLƏŞMİŞ KRALLIĞ',
        'footer.demo_text': 'Bu Demo həmçinin',
        'footer.theme_name': 'Kallyas WordPress Mövzusunun bir hissəsidir',
        'footer.company': 'Hogash.com',
        
        // Menu Page
        'menu.banner.restaurant': 'Restoran',
        'menu.banner.menu': 'Menyu',
        'menu.banner.juicy': 'Dadlı Təamlar',
        'menu.asparagus': 'Quşqonmaz və Halloumi Salatı',
        'menu.weight': '(250 Q)',
        'menu.ingredients': 'Lobya / Halloumi pendiri / Quşqonmaz / Kabak / Kiraz pomidorları',
        'menu.tasty': 'Dadlı',
        'menu.default_sorting': 'Standart çeşidləmə',
        'menu.sort_popularity': 'Populyarlığa görə çeşidlə',
        'menu.sort_rating': 'Reytinqə görə çeşidlə',
        'menu.sort_price_low': 'Qiymətə görə: aşağıdan yuxarı',
        'menu.sort_price_high': 'Qiymətə görə: yuxarıdan aşağı',
        
        // Reservation Page
        'reservation.banner.make': 'online',
        'reservation.banner.title': 'Rezervasiya',
        'reservation.banner.call': 'Və ya bizə zəng edin',
        'reservation.banner.time': '10:00 - 19:00',
        'reservation.form.title': 'Şam yeməyi üçün yer rezerv edin!',
        'reservation.form.name': 'Adınız',
        'reservation.form.phone': 'Telefon nömrəniz',
        'reservation.form.date': 'Tarix',
        'reservation.form.time': 'Vaxt',
        'reservation.form.guests': 'Qonaq sayı',
        'reservation.form.submit': 'İNDİ REZERV ET',
        
        // Shop Page
        'shop.banner.restaurant': 'Restoran',
        'shop.banner.shop': 'Mağaza',
        'shop.title': 'Mağaza',
        'shop.showing': '14 NƏTİCƏDƏN 1-11 GÖSTƏRİLİR',
        'shop.next': 'NÖVBƏTİ',
        
        // Contact Page
        'contact.banner.restaurant': 'Restoran',
        'contact.banner.contact': 'Əlaqə',
        'contact.banner.juicy': 'dadlı təamlar',
        'contact.details': 'əlaqə məlumatları',
        'contact.form.name': 'ad',
        'contact.form.email': 'Email',
        'contact.form.message': 'Mesaj',
        'contact.form.submit': 'Mesaj Göndər',

        // Home Page
        'home.banner.welcome': 'Xoş gəlmisiniz',
        'home.banner.restaurant': 'restoran',
        'home.banner.name': "danny's",
        'home.banner.aboutUs': 'haqqımızda',
        'home.banner.readMore': 'ətraflı',
        
        // Menu Section
        'home.menu.restaurant': 'Restoran',
        'home.menu.menu': 'menyu',
        'home.menu.whatsOn': 'menyuda nə var',
        'home.menu.viewAll': 'hamısına bax',
        'home.menu.addToCart': 'Səbətə əlavə et',
        
        // Specialties Section
        'home.specialties.chinese': 'Çin',
        'home.specialties.cuisine': 'mətbəxi',
        'home.specialties.title': 'xüsusi təkliflər',
        'home.specialties.todaySpecial': 'günün xüsusi təklifi',
        'home.specialties.creme': 'Ən yaxşıların ən yaxşısı',
        'home.specialties.roastedDuck': 'Xüsusi sousla sobada bişmiş ördək',
        'home.specialties.readMore': 'ətraflı',
        'home.specialties.description': 'Yeməklərimiz ən keyfiyyətli təzə məhsullardan hazırlanır',
        
        // Testimonials Section
        'home.testimonials.clients': 'Müştərilər',
        'home.testimonials.testimonials': 'rəylər',
        'home.testimonials.author': 'ceyms de franko',
        'home.testimonials.text': 'Baş aşbaz Taylor Bonnyman, baş aşbaz Margaret Keogh ilə birlikdə zərif və əyləncəli müasir Britaniya mətbəxini təqdim edir.',
        
        // Reservation Section
        'home.reservation.title': 'rezervasiyalar',
        'home.reservation.address': '49 Featherstone Street LONDON EC1Y 8SY UNITED KINGDOM',
        'home.reservation.phone': '+4 1800 555 1234',
        'home.reservation.email': 'bookatable@restaurant.com',
        'home.reservation.online': 'ONLİNE',
        
        // Subscribe Section
        'home.subscribe.discount': '15% ENDİRİM',
        'home.subscribe.title': 'Yeniliklərimizə',
        'home.subscribe.newsletter': 'abunə olun',
        'home.subscribe.description': 'Yeniliklərimizə abunə olun və sifarişinizdən 15% endirim əldə edin',
        'home.subscribe.placeholder': 'Sizin@poçt.com',
        'home.subscribe.button': 'abunə ol',

        // Categories
        'home.category.all': 'hamısı',
        'home.category.starters': 'başlanğıclar',
        'home.category.main': 'əsas yeməklər',
        'home.category.dessert': 'desertlər',
        'home.category.drinks': 'içkilər',
        'home.category.dailyMenu': 'günün menyusu',
        'home.category.specialty': 'şef təklifi',
        'home.category.offers': 'təkliflər',

        // Menu Items
        'home.menu.item1.title': 'Quzuqulağı və Halloumi pendiri salatı',
        'home.menu.item1.weight': '(250 Q)',
        'home.menu.item1.ingredients': 'Lobya / Halloumi pendiri / Quzuqulağı / Balqabaq / Çeri pomidor',
        'home.menu.item1.price': '22.00 ₼',

        'home.menu.item2.title': 'Sarımsaqlı qızardılmış krevetlər',
        'home.menu.item2.weight': '(200 Q)',
        'home.menu.item2.ingredients': 'Krevetlər / Sarımsaq / Yağ / Limon / Göyərti',
        'home.menu.item2.price': '35.00 ₼',

        // Footer Additional
        'footer.address.street': '49 Featherstone Küçəsi',
        'footer.address.city': 'LONDON',
        'footer.address.postal': 'EC1Y 8SY',
        'footer.address.country': 'BİRLƏŞMİŞ KRALLIĞ',
        'footer.demo_text': 'Bu Demo həmçinin',
        'footer.theme_name': 'Kallyas WordPress Mövzusunun bir hissəsidir',
        'footer.company': 'Hogash.com',
        'footer.rights': 'Bütün hüquqlar qorunur'
    },
    ru: {
        // Navigation
        'nav.home': 'Главная',
        'nav.menu': 'Меню',
        'nav.reservation': 'Бронирование',
        'nav.blog': 'Блог',
        'nav.shop': 'Магазин',
        'nav.contact': 'Контакты',
        
        // Header
        'header.login': 'Вход',
        'header.register': 'Регистрация',
        'header.items': 'ТОВАР(Ы)',
        'header.total': 'Итого',
        
        // Menu Section
        'menu.title': 'Ресторан',
        'menu.subtitle': 'меню',
        'menu.whatsOnMenu': 'что в меню',
        
        // Specialties Section
        "specialties.title": "Cпециальности",
        'specialties.chinese': 'Китайская',
        'specialties.cuisine': 'кухня',
        'specialties.description': 'Наши блюда готовятся из свежих ингредиентов высочайшего качества',
        "specialties.creme": "Лучшие сливки",
        "specialties.todaySpecial": "Cегодняшняя Cпециальность",
        "specialties.roastedDuck": "Утка, запеченная в духовке, с фирменным соусом",
        "specialties.readMore": "Читать далее",
        "menuList.restaurant": "Pесторан",
        "menuList.menu": "Меню",
        
        // Subscribe Section
        'subscribe.title': 'Подпишитесь на нашу',
        'subscribe.newsletter': 'рассылку',
        'subscribe.discount': 'Подпишитесь на нашу рассылку и получите скидку 15% на ваш заказ.',
        'subscribe.placeholder': 'Ваш@почта.com',
        'subscribe.button': 'подписаться',
        
        // Footer
        'footer.chefInfo': 'Шеф-повар Тейлор Бонниман, работая в сотрудничестве с шеф-поваром Маргарет Кеог, предлагает элегантную и игривую современную британскую кухню.',
        'footer.follow': 'следить',
        'footer.onlineReservation': 'онлайн бронирование',
        'footer.copyright': 'авторское право © 2016',
        'footer.rights': 'Все права защищены.',
        
        // Blog Page
        'blog.restaurant': 'Ресторан',
        'blog.blog': 'Блог',
        'blog.subtitle': 'Мы готовим и пишем для вас',
        'blog.restaurant_name': "Danny's",
        'blog.categories': 'Категории',
        'blog.all': 'Все',
        'blog.chef_specialty': 'ФИРМЕННЫЕ БЛЮДА',
        'blog.drinks': 'НАПИТКИ',
        'blog.desert': 'ДЕСЕРТЫ',
        'blog.main': 'ОСНОВНЫЕ БЛЮДА',
        'blog.offers': 'АКЦИИ',
        'blog.starters': 'ЗАКУСКИ',
        'blog.daily_menu': 'МЕНЮ ДНЯ',
        'blog.follow_us': 'Следите за нами',
        'blog.newsletter_title': 'РАССЫЛКА',
        'blog.newsletter_subtitle': 'Подпишитесь и получите купон на скидку 15%',
        'blog.featured': 'РЕКОМЕНДУЕМЫЕ',
        'blog.under': 'в категории',
        'blog.special_deal': 'Специальное предложение при заказе 3 и более пицц',
        'blog.posted_date': '15 ИЮНЯ, 2016',
        'blog.posted_by': 'автор',
        'blog.author': 'МАРИУС Х',
        'blog.in_category': 'в',
        'blog.category_photo': 'ФОТОГРАФИЯ',
        'blog.lorem': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
        'blog.subscribe_btn': 'ПОДПИСАТЬСЯ',
        'blog.email_placeholder': 'ваш.адрес@почта.com',
        
        // Footer Additional
        'footer.address.phone': '+4 1800 555 1234',
        'footer.address.email': 'bookatable@restaurant.com',
        'footer.address.street': '49 Featherstone Street',
        'footer.address.city': 'LONDON',
        'footer.address.postal': 'EC1Y 8SY',
        'footer.address.country': 'UNITED KINGDOM',
        'footer.demo_text': 'This Demo is also part of',
        'footer.theme_name': 'Kallyas WordPress Theme',
        'footer.company': 'Hogash.com',
        
        // Menu Page
        'menu.banner.restaurant': 'Ресторан',
        'menu.banner.menu': 'Меню',
        'menu.banner.juicy': 'Сочные блюда',
        'menu.asparagus': 'Салат из спаржи и сыра халуми',
        'menu.weight': '(250 Г)',
        'menu.ingredients': 'Фасоль / Сыр халуми / Спаржа / Цукини / Помидоры черри',
        'menu.tasty': 'Вкусно',
        'menu.default_sorting': 'Сортировка по умолчанию',
        'menu.sort_popularity': 'По популярности',
        'menu.sort_rating': 'По рейтингу',
        'menu.sort_price_low': 'По возрастанию цены',
        'menu.sort_price_high': 'По убыванию цены',
        'menu.addToCart': 'В корзину',
        'menu.next': 'ДАЛЕЕ',
        'menu.whatsOnMenu': 'Что в меню',
        'menu.viewAll': 'Смотреть все',

        // Reservation Page
        'reservation.banner.make': 'Сделать онлайн',
        'reservation.banner.title': 'Бронирование',
        'reservation.banner.call': 'Или позвоните нам',
        'reservation.banner.time': '10:00 - 19:00',
        'reservation.form.title': 'Забронируйте столик для ужина!',
        'reservation.form.name': 'Ваше имя',
        'reservation.form.phone': 'Ваш номер телефона',
        'reservation.form.date': 'Дата',
        'reservation.form.time': 'Время',
        'reservation.form.guests': 'Количество гостей',
        'reservation.form.submit': 'ЗАБРОНИРОВАТЬ',

        // Shop Page
        'shop.banner.restaurant': 'Ресторан',
        'shop.banner.shop': 'Магазин',
        'shop.title': 'Магазин',
        'shop.showing': 'ПОКАЗАНО 1-11 ИЗ 14 РЕЗУЛЬТАТОВ',
        'shop.next': 'ДАЛЕЕ',
        'shop.filter': 'Фильтр',
        'shop.sort_default': 'Сортировка по умолчанию',
        'shop.sort_popularity': 'По популярности',
        'shop.sort_rating': 'По рейтингу',
        'shop.sort_price_low': 'По возрастанию цены',
        'shop.sort_price_high': 'По убыванию цены',
        'shop.addToCart': 'В корзину',

        // Contact Page
        'contact.banner.restaurant': 'Ресторан',
        'contact.banner.contact': 'Контакты',
        'contact.banner.juicy': 'сочные блюда',
        'contact.details': 'Контактная информация',
        'contact.form.name': 'Имя',
        'contact.form.email': 'Email',
        'contact.form.message': 'Сообщение',
        'contact.form.submit': 'Отправить сообщение',
        'contact.address': 'Адрес',
        'contact.phone': 'Телефон',
        'contact.email': 'Email',
        'contact.hours': 'Часы работы',
        'contact.hours.weekdays': 'ПН-ПТ: 10:00 - 22:00',
        'contact.hours.weekend': 'СБ-ВС: 11:00 - 23:00',

        // Categories
        'category.all': 'Все',
        'category.starters': 'Закуски',
        'category.main': 'Основные блюда',
        'category.dessert': 'Десерты',
        'category.drinks': 'Напитки',
        'category.dailyMenu': 'Меню дня',
        'category.specialty': 'Фирменные блюда',
        'category.offers': 'Акции',

        // Restaurant Section
        'restaurant.welcome': 'Добро пожаловать в',
        'restaurant.name': "ресторан danny's",
        'restaurant.aboutUs': 'о нас',

        // Home Page
        'home.banner.welcome': 'Добро пожаловать в',
        'home.banner.restaurant': 'ресторан',
        'home.banner.name': "danny's",
        'home.banner.aboutUs': 'о нас',
        'home.banner.readMore': 'Подробнее',
        
        // Menu Section
        'home.menu.restaurant': 'Ресторан',
        'home.menu.menu': 'меню',
        'home.menu.whatsOn': 'что в меню',
        'home.menu.viewAll': 'смотреть все',
        'home.menu.addToCart': 'В корзину',
        
        // Specialties Section
        'home.specialties.chinese': 'Китайская',
        'home.specialties.cuisine': 'кухня',
        'home.specialties.title': 'фирменные блюда',
        'home.specialties.todaySpecial': 'специальное предложение дня',
        'home.specialties.creme': 'Лучшее из лучшего',
        'home.specialties.roastedDuck': 'Утка, запеченная в духовке с особым соусом',
        'home.specialties.readMore': 'подробнее',
        'home.specialties.description': 'Наши блюда готовятся из свежих ингредиентов высочайшего качества',
        
        // Testimonials Section
        'home.testimonials.clients': 'Клиенты',
        'home.testimonials.testimonials': 'отзывы',
        'home.testimonials.author': 'джеймс де франко',
        'home.testimonials.text': 'Шеф-повар Тейлор Бонниман, работая в сотрудничестве с шеф-поваром Маргарет Кеог, предлагает элегантную и игривую современную британскую кухню.',
        
        // Reservation Section
        'home.reservation.title': 'бронирование',
        'home.reservation.address': 'ул. Фезерстоун, 49, ЛОНДОН EC1Y 8SY ВЕЛИКОБРИТАНИЯ',
        'home.reservation.phone': '+4 1800 555 1234',
        'home.reservation.email': 'bookatable@restaurant.com',
        'home.reservation.online': 'ОНЛАЙН',
        
        // Subscribe Section
        'home.subscribe.discount': 'СКИДКА 15%',
        'home.subscribe.title': 'Подпишитесь на нашу',
        'home.subscribe.newsletter': 'рассылку',
        'home.subscribe.description': 'Подпишитесь на нашу рассылку и получите скидку 15% на ваш заказ',
        'home.subscribe.placeholder': 'Ваш@почта.com',
        'home.subscribe.button': 'подписаться',

        // Categories
        'home.category.all': 'все',
        'home.category.starters': 'закуски',
        'home.category.main': 'основные блюда',
        'home.category.dessert': 'десерты',
        'home.category.drinks': 'напитки',
        'home.category.dailyMenu': 'меню дня',
        'home.category.specialty': 'фирменные блюда',
        'home.category.offers': 'акции',

        // Menu Items
        'home.menu.item1.title': 'Салат из спаржи и сыра халуми',
        'home.menu.item1.weight': '(250 Г)',
        'home.menu.item1.ingredients': 'Фасоль / Сыр халуми / Спаржа / Цукини / Помидоры черри',
        'home.menu.item1.price': '22.00 ₽',

        'home.menu.item2.title': 'Жареные креветки с чесноком',
        'home.menu.item2.weight': '(200 Г)',
        'home.menu.item2.ingredients': 'Креветки / Чеснок / Масло / Лимон / Зелень',
        'home.menu.item2.price': '35.00 ₽',

        // Footer
        'footer.address.street_ru': 'ул. Фезерстоун, 49',
        'footer.address.city_ru': 'ЛОНДОН',
        'footer.address.postal_ru': 'EC1Y 8SY',
        'footer.address.country_ru': 'ВЕЛИКОБРИТАНИЯ',
        'footer.demo_text_ru': 'Это демо также является частью',
        'footer.theme_name_ru': 'темы WordPress Kallyas',
        'footer.company_ru': 'Hogash.com'
    }
};

function language() {
    const languageButton = document.querySelector('.language-button');
    const dropdown = document.getElementById('languageDropdown');
    const languageOptions = dropdown.querySelectorAll('a');
    const span = document.querySelector('.language-button span');

    // Toggle dropdown
    languageButton.addEventListener('click', () => {
        dropdown.classList.toggle('show');
    });

    // Close dropdown when clicking outside
    window.addEventListener('click', (e) => {
        if (!e.target.matches('.language-button') && !e.target.matches('.language-button *')) {
            dropdown.classList.remove('show');
        }
    });

    // Handle language selection
    languageOptions.forEach(option => {
        option.addEventListener('click', (e) => {
            e.preventDefault();
            const lang = option.getAttribute('data-lang');
            span.innerText = lang.toUpperCase();
            localStorage.setItem('language', lang);

        languageItems.forEach((item) => {
            const key = item.getAttribute('data-language');
                item.innerText = translations[lang][key];
            });

            dropdown.classList.remove('show');
        });
    });
}

const savedLang = localStorage.getItem('language') || 'en';
span.innerText = savedLang.toUpperCase();

languageItems.forEach((item) => {
    const key = item.getAttribute('data-language');
    item.innerText = translations[savedLang][key];
});

language();

