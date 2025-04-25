//// server.js
//const express = require('express');
//const app = express();
//const stripe = require('stripe')('sk_test_51RDV6rB03cO7vFcX3BQEF3AnAl4T3KRLcywg64jCWICUZddSczmJmeObEqFYT1XkjA9RnDlSU9RPTHJZaUZa2un700fnJEObBk'); // BURAYA ÖZ Stripe Secret Key-ni yaz
//const cors = require('cors');

//app.use(cors());
//app.use(express.json());

//app.post('/create-checkout-session', async (req, res) => {
//  const session = await stripe.checkout.sessions.create({
//    payment_method_types: ['card'],
//    line_items: [{
//      price_data: {
//        currency: 'usd',
//        product_data: {
//          name: 'Sample Product',
//        },
//        unit_amount: 154349,
//      },
//      quantity: 1,
//    }],
//    mode: 'payment',
//    success_url: 'https://your-site.com/success',
//    cancel_url: 'https://your-site.com/cancel',
//  });

//  res.json({ id: session.id });
//});

//app.listen(4242, () => console.log('Server running on port 4242'));
