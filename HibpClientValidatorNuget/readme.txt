----------------------------------
HibpClientValidator
----------------------------------

Make sure to include the hibpvalidator.js in one of your bundles, or the client validation will not work.

Additionally you need to reference the sha1.js, the easiest way is to use the cloudflare js cdn:


<script src="https://cdnjs.cloudflare.com/ajax/libs/js-sha1/0.6.0/sha1.js" integrity="sha256-LmIVkNdxjrHbViQZD9LSewZc+3rU/alc7P/UJj6mUPc=" crossorigin="anonymous"></script>


For asp.net core projects it will not install the javascript file automatically, so you will have to download and import it into your project manually, untill we have a solution.

The client javascript can be downloaded here:
https://github.com/zealofzebras/HibpClientValidator/blob/master/HibpClientValidatorNuget/hibpvalidator.core.js


In the models that you want to check (hint: RegisterViewModel and ChangePasswordViewModel are good candidates)

add
using HibpClientValidator;


and decorate the appropriate properties with the Hibp attribute:
[Hibp("This password has been exposed in password leaks, please choose another", "Checking password...")]
