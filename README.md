.NET SDK for Mondido Payments
=======

Version 1.0   
.NET version 4.5, Visual Studio 2012

The SDK provides developers with a easy-to-use library to make payments in their .NET Server or Windows Phone application. 
Open the included unit tests and see how it works.


Example:
``` csharp
//get one transaction   
var transaction = Transaction.Get(1);

//get three, from the top   
var transactions = Transaction.List(3,0);

//create a payment using encrypted card number
var payment_ref = DateTimeOffset.Now.Ticks.ToString();
var postData = new List<KeyValuePair<string, string>>();
var encryptedCard = "4111111111111111".RSAEncrypt();

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

var transaction = Transaction.Create(postData);
```

Read more at https://mondido.com/documentation
