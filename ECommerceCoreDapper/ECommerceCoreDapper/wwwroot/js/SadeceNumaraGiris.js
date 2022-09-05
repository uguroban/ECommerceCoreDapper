//sadece rakam girilmesi gereken alanı kontrol eden fonksiyon
function numberonly(myfield, e, dec) {
var key;
var keychar;

 if (window.event)
key = window.event.keyCode;
else if (e)
key = e.which;
else
return true;
keychar = String.fromCharCode(key);

 // control keys
if ((key == null) || (key == 0) || (key == 8) ||
(key == 9) || (key == 13) || (key == 27))
return true;

 // numbers
else if ((("0123456789").indexOf(keychar) > -1))
return true;

 // decimal point jump
else if (dec && (keychar == ".")) {
myfield.form.elements[dec].focus();
alert("Lütfen sadece rakam giriniz.");
return false;
}
else {
alert("Lütfen sadece rakam giriniz.");
return false;
}
}