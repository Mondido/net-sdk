Please find more information about making payments and refunds in our API documentation:
https://www.mondido.com/documentation/api
     
IMPORTANT: 

Mondido is a certified payment provider compliant to Payment Card Industry Data Security Standard (PCI DSS) to provide a secure transaction for merchants and their customers.
PCI compliance for merchants is required for any business accepting cardholder data. 
     
We strongly recommend NOT sending card numbers unencrypted to and from your server.
Let Mondido capture this sensitive information using one of our hosted window or .js solutions to avoid PCI compliance issues.
https://doc.mondido.com/hosted
https://doc.mondido.com/mondidojs



CONFIGURATION:

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

This SDK require:
Newtonsoft JSON.NET (https://www.nuget.org/packages/Newtonsoft.Json/)
Bouncy Castle (https://www.nuget.org/packages/BouncyCastle/)


Do not hesitate to contact help@mondido.com for any questions!
