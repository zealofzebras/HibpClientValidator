# HibpClientValidator
Client Validator for MVC (both Core and Framework) that validates passwords against the https://haveibeenpwned.com/ password database. It will not tell how many times a password has been used as it is not relevant. If it has been leaked, it should not be used again.

## Installation

```powershell
Install-Package Sustainable.Web.Mvc.HibpClientValidator
```

## Usage

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