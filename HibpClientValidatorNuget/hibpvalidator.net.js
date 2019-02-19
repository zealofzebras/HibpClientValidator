// JavaScript source code
$(function () {
    var lastCount = 0;
    jQuery.validator.unobtrusive.adapters.add('hibp',
        ['element'],
        function (options) {
            options.rules['remote'] = function (element) { 
                var password = $(element).val();
                var hash = sha1(password).toUpperCase();
                var prefix = hash.slice(0, 5);
                var suffix = hash.slice(5);

                $(element).parent().find("#Password-error").text("checking....");
                $(element).removeClass("ignore-hibp");

                var r = {
                    url: "https://api.pwnedpasswords.com/range/" + prefix,
                    type: "get",
                    cache: false,
                    dataFilter: function (response) {
                        var results = response.split(/\r?\n/);
                        for (i = 0; i < results.length; i++) {
                            var line = results[i].split(":");
                            if (line[0] === suffix) {
                                lastCount = line[1];
                                options.messages['remote'] = options.message + line[1];
                                $(element).addClass("ignore-hibp");
                                return false;
                            }
                        }
                        return true;
                    }
                };
                return r;
            };
            options.ignore = ".ignore-hibp";
            options.messages['remote'] = function () {
                return options.message + ' <a href="https://haveibeenpwned.com/API/v2#SearchingPwnedPasswordsByPassword" target="_blank" class="hibp-info" data-toggle="tooltip" data-html="true" title="This is an automated check to ensure that your password has not already been compromised, the information was obtained from [Have I been pwned?] and was checked completely anonymously and without transfering your password">(what is this?)</a>';
            };
        });
}(jQuery));