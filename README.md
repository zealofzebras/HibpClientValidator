# HibpClientValidator
Unobtrusive jQuery Validation plugin that validates passwords against the https://haveibeenpwned.com/ password database. It will not tell how many times a password has been used as it is not relevant. If it has been leaked, it should not be used again.

Additionally a Client Validator for MVC (both Core and Framework) is provided

## Usage and installation in Asp.Net MVC

### Installation

```powershell
Install-Package Sustainable.Web.Mvc.HibpClientValidator
```

### Usage

In the models that you want to check (hint: `RegisterViewModel` and `ChangePasswordViewModel` are good candidates)

add
```csharp
using HibpClientValidator;
```

and decorate the appropriate properties with the Hibp attribute:

```csharp
[Hibp("This password has been exposed in password leaks, please choose another", "Checking password...")]
```

Make sure to add `hibpvalidator.js` and `sha1.js` to one of your bundles or directly to your form pages:

```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/js-sha1/0.6.0/sha1.js" integrity="sha256-LmIVkNdxjrHbViQZD9LSewZc+3rU/alc7P/UJj6mUPc=" crossorigin="anonymous"></script>
<script src="~/scripts/hibpvalidator.js"></script>
```

## Usage and Installation in javascript

### Installation

Get the library from NPM (https://www.npmjs.com/package/hibpvalidator) or download the `hibpvalidator.js` and reference `sha1.js` additional requirements are jQuery, jQuery validation and jQuery unobtrusive validation.

Include them in your project
```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.0/jquery.validate.min.js" integrity="sha256-bu/BP02YMudBc96kI7yklc639Mu4iKGUNNcam8D2nLc=" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js" integrity="sha256-9GycpJnliUjJDVDqP0UEu/bsm9U+3dnQUH8+3W10vkY=" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/js-sha1/0.6.0/sha1.js" integrity="sha256-LmIVkNdxjrHbViQZD9LSewZc+3rU/alc7P/UJj6mUPc=" crossorigin="anonymous"></script>
<script src="~/scripts/hibpvalidator.js"></script>
```

Then decorate your html forms with unobtrusive validation arguments

```html
<input type="password" 
    data-val="true" 
    data-val-hibp="This password has been exposed in password leaks, please choose another" 
    data-val-hibp-checking="Checking password..." data-val-length="The Password must be at least 6 and at max 100 characters long." 
    data-val-length-max="100" 
    data-val-length-min="6" 
    data-val-required="The Password field is required." id="Password" name="Password" />
<span class="text-danger field-validation-valid" 
    data-valmsg-for="Password" 
    data-valmsg-replace="true"></span>
```

## Alternatives

Why should you use this instead of something like:
https://www.nuget.org/packages/Matrixsoft.PwnedPasswords/

Since this is a client validator, it runs in the users browser. This creates a better user experience (it is faster) and the bandwith is reduced from the server. It is damn easy to implement.

## Accolades

Obviously this is a small wrapper for the massive work done by Troy Hunt:
* https://www.troyhunt.com/
* https://haveibeenpwned.com/

## Demonstration

The package is in live use on these sites (make a PR if you know more)
* https://angled.ca/Account/Register