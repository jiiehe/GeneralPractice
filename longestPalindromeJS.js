/**
 * @param {string} s
 * @return {string}
 */
var longestPalindrome = function (s) {
    var result = "";
    for (var i = 0; i < s.length; i++) {
        
        var left = i;
        var right = i;
        while (left > 0 && right < s.length-1&& s[left-1] == s[right+1]) {
            left--;
            right++;
        }
        if (right - left + 1 > result.length) {
            result = s.substr(left, right - left + 1);
        }
    }
    for (var i = 0; i < s.length - 1; i++) {
        if (s[i] == s[i + 1]) {
            var result = "";
            var left = i;
            var right = i+1;
            while (left > 0 && right < s.length-1 && s[left-1] == s[right+1]) {
                left--;
                right++;
            }
            if (right - left + 1 > result.length) {
                result = s.substr(left, right - left + 1);
            }
        }
    }
    return result;

};
console.log(longestPalindrome("cbbd"));
