.NET SDK for Mondido Payments
=======

Version 2.3

Parsing of returned object in error case

Version 2.2

Updated error handling, that adds a property 'ApiError' which contain any error messages or HTTP codes.


Version 2.1

.NET version 4.5

The SDK provides developers with a easy-to-use library to make payments in their .NET Server or Windows Phone / Xamarin application. 
Open the included unit tests and see how it works.

CHANGELOG
---
2017-08-14
- Added RestSharp 

2017-01-31
- Added support for items, metadata, auth and capture for Transaction
- Added Subscriptions, Plans, Customers

Example, preparing a payment for Hosted Window:
``` csharp

//prepare a payment
var payment_ref = DateTimeOffset.Now.Ticks.ToString();
var customer_ref = "MyCustomer123";
var currency = "eur";
var test = "true";
var postData = new List<KeyValuePair<string, string>>();
var process = "false"

postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
postData.Add(new KeyValuePair<string, string>("customer_ref", customer_ref));
postData.Add(new KeyValuePair<string, string>("test", test));
postData.Add(new KeyValuePair<string, string>("currency", currency));
postData.Add(new KeyValuePair<string, string>("locale", "en"));
postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + customer_ref + "10.00" + currency + (test.Equals("true") ? "test" : "" ) + Settings.ApiSecret).ToMD5()));
postData.Add(new KeyValuePair<string, string>("process", process)); // We don't want to process, but only get the URL for later card/invoice/other collecting.

var newTransaction = Mondido.Payment.Transaction.Create(postData);
var redirect_the_customer_to_this_url = newTransaction.Href;
//it will look like this: https://pay.mondido.com/v1/form/Wcxn78Ow5EkxsAS4rIdx5w

```

Example card payment:
``` csharp
//get one transaction   
var transaction = Mondido.Payment.Transaction.Get(1);

//get three, from the top   
var transactions = Mondido.Payment.Transaction.List(3,0);

//create a payment using encrypted card number
var payment_ref = DateTimeOffset.Now.Ticks.ToString();
var customer_ref = "Customer Reference Test";
var currency = "sek";
var test = "true";
var postData = new List<KeyValuePair<string, string>>();
var encryptedCard = "4111111111111111".RSAEncrypt();//DO NOT COLLECT CARD NUMBERS OR SEND CARD NUMBERS IN CLEAR TEXT. 
//You should set encryptedCard to a tokenized card number and use it as recurring.

postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
postData.Add(new KeyValuePair<string, string>("customer_ref", customer_ref));
postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
postData.Add(new KeyValuePair<string, string>("card_holder", ".net sdk"));
postData.Add(new KeyValuePair<string, string>("test", test));
postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
postData.Add(new KeyValuePair<string, string>("card_number", encryptedCard));
postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
postData.Add(new KeyValuePair<string, string>("currency", currency));
postData.Add(new KeyValuePair<string, string>("locale", "en"));
postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + customer_ref + "10.00" + currency + (test.Equals("true") ? "test" : "" ) + Settings.ApiSecret).ToMD5()));
postData.Add(new KeyValuePair<string, string>("encrypted", "card_number"));

var newTransaction = Mondido.Payment.Transaction.Create(postData);
```

Read more at https://mondido.com/documentation

IMPORTANT: 
---
Mondido is a certified payment provider compliant to Payment Card Industry Data Security Standard (PCI DSS) to provide a secure transaction for merchants and their customers.
PCI compliance for merchants is required for any business accepting cardholder data. 
     
We strongly recommend NOT sending card numbers unencrypted to and from your server.
Let Mondido capture this sensitive information using one of our hosted window or .js solutions to avoid PCI compliance issues.   
https://www.mondido.com/documentation/hosted   
https://www.mondido.com/documentation/mondidojs   


3D-SECURE:
---
Since many card transactions require 3D-Secure it is highly recommend that this SDK is implented to prepare payments and refunds, but collecting card data should be done in a Hosted Window environment so that 3D-Secure can be handled by Mondido.



CONFIGURATION:
---
First sign up and create your merchant account at: https://www.mondido.com/registrera

#### Settings

##### Api BaseUrl

https://api.mondido.com/v1

##### Api Username

The merchant id found at: https://www.mondido.com/en/settings#api after you have logged in and created a merchant account

##### Api Password

The merchant password that you have set at: https://www.mondido.com/en/settings#api after you have logged in and created a merchant account

##### Api Secret

The merchant secret that you can find at: https://www.mondido.com/en/settings#api after you have logged in and created a merchant account

##### RSAKey

The merchant RSA public key that you can find at: https://www.mondido.com/en/settings#api after you have logged in and created a merchant account

===========


DEPENDENCIES:
---
This SDK require:   
Newtonsoft JSON.NET (https://www.nuget.org/packages/Newtonsoft.Json/)   
Bouncy Castle (https://www.nuget.org/packages/BouncyCastle/)   

# Mondido Payments Documentation

Our focus is to make it as smooth as possible for you to implement Mondido and start accepting payments, regardless of whether you are implementing from scratch or already have an existing payment service in place.

Read more
* https://doc.mondido.com/

## Supported Card Types
Default card types that you will have access to are VISA and Mastercard, but the other such as AMEX, JCB and Diners are on separate contracts. Contact support for more information about card types.

* https://doc.mondido.com/api#cardtypes

## Test Cards
To create test transactions you need to send in a test card number, and also a CVV code that can simulate different responses

* https://doc.mondido.com/api#testcards

## Error messages
We aim to send as many insightful and helpful error messages to you as possible, both in numeric, data and human readable.

* https://doc.mondido.com/api#errors

# Help

* FAQ (Swedish) - http://help.mondido.com/

# PCI DSS

Mondido is a certified payment provider compliant to Level 1 Payment Card Industry Data Security Standard (PCI DSS) version 3.1 to provide a secure transaction for merchants and their customers. PCI compliance for merchants is required for any business accepting cardholder data. Let Mondido capture this sensitive information using one of our Hosted Window or mondido.js solutions to avoid PCI compliance issues.

* Payment Card Industry Data Security Standard (PCI DSS) - https://www.pcicomplianceguide.org/pci-faqs-2/#5
* Payment security educational resources - https://www.pcisecuritystandards.org/pci_security/educational_resources
* Hosted Window - https://doc.mondido.com/hosted

# 3D-Secure

Mondido understands the need to incorporate best business practices in security. That's why we've made it easy for merchants to implement 3D Secure or “3 Domain Secure” as the industry standard identity check solution to minimize chargebacks from fraudulent credit cards, all included in our simple pricing. 3D-Secure refers to second authentication factor products such as Verified by Visa, MastercardⓇSecureCode™, American Express SafekeyⓇ, and JCB J/Secure™.

NOTE: While you can create your own payment experience, We strongly recommend using our Hosted Window or Mondido.js solution to save time in implementing 3D-Secure and client side encryption to your checkout procedure.

* Verified by Visa - http://www.visaeurope.com/making-payments/verified-by-visa/
* MastercardⓇSecureCode™ - https://www.mastercard.us/en-us/merchants/safety-security.html
* American Express SafekeyⓇ  - https://www.americanexpress.com/uk/content/safekey-information.html?linknav=uk-securitycentre-home-safekey-learn
* JCB J/Secure™ - http://www.global.jcb/en/

# SSL

Secure Socket Layer is required to securely transfer cardholder data and payment information to Mondido. It is recommended that you purchase a SSL certificate directly through a recognized certification authority such as TrustwaveⓇ, HTTPS.SE or purchase a custom SSL certificate through your current e-commerce solution.

* TrustwaveⓇ - https://ssl.trustwave.com/buy-ssl-certificate?___s=1
* HTTPS.SE - https://https.se/

# Follow us on
* GitHub - https://github.com/Mondido
* Facebook - https://www.facebook.com/mondidopayments
* Twitter https://twitter.com/mondidopay
* LinkedIn  - https://www.linkedin.com/company/mondido
* Instagram - https://www.instagram.com/mondidopay/


Do not hesitate to contact support@mondido.com for any questions!
