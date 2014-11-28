.NET SDK for Mondido Payments
=======

Version 1.1   
.NET version 4.5, Visual Studio 2012

The SDK provides developers with a easy-to-use library to make payments in their .NET Server or Windows Phone application. 
Open the included unit tests and see how it works.


Example:
``` csharp
//get one transaction   
var transaction = Mondido.CreditCard.Transaction.Get(1);

//get three, from the top   
var transactions = Mondido.CreditCard.Transaction.List(3,0);

//create a payment using encrypted card number
var payment_ref = DateTimeOffset.Now.Ticks.ToString();
var postData = new List<KeyValuePair<string, string>>();
var encryptedCard = "4111111111111111".RSAEncrypt();//DO NOT SEND CARD NUMBERS IN CLEAR TEXT

postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
postData.Add(new KeyValuePair<string, string>("card_holder", ".net sdk"));
postData.Add(new KeyValuePair<string, string>("test", "true"));
postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
postData.Add(new KeyValuePair<string, string>("card_number", encryptedCard));
postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
postData.Add(new KeyValuePair<string, string>("currency", "sek"));
postData.Add(new KeyValuePair<string, string>("locale", "en"));
postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + "10.00" + "sek" + Settings.ApiSecret).ToMD5()));
postData.Add(new KeyValuePair<string, string>("encrypted", "card_number"));

var newTransaction = Mondido.CreditCard.Transaction.Create(postData);
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


CONFIGURATION:
---
First sign up and create your merchant account at: https://www.mondido.com

The merchant account specific information is loaded from a app/web.config file of your project.
They are found in the Admin portal:
https://www.mondido.com/en/settings 

ApiBaseUrl : https://api.mondido.com/v1   
ApiUsername : the merchant id found at: https://www.mondido.com/en/settings after you have logged in and created a merchant account   
ApiPassword : the merchant password that you have set at: https://www.mondido.com/en/settings after you have logged in and created a merchant account   
ApiSecret :  the merchant secret that you can find at: https://www.mondido.com/en/settings after you have logged in and created a merchant account   
RSAKey : the merchant RSA public key that you can find at: https://www.mondido.com/en/settings after you have logged in and created a merchant account   


DEPENDENCIES:
---
This SDK require:
Newtonsoft JSON.NET (https://www.nuget.org/packages/Newtonsoft.Json/)   
Bouncy Castle (https://www.nuget.org/packages/BouncyCastle/)   


Do not hesitate to contact support@mondido.com for any questions!

