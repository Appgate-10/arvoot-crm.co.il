function insertAtCursor(myField, startValue, endValue) {
    //IE support
    if (document.selection) {
        myField.focus();
        sel = document.selection.createRange();
        sel.text = startValue;
    }
        //MOZILLA and others
    else if (myField.selectionStart || myField.selectionStart == '0') {
        var startPos = myField.selectionStart;
        var endPos = myField.selectionEnd;

        myField.value = myField.value.substring(0, startPos)
            + startValue
            + myField.value.substring(endPos - (endPos - startPos), myField.value.length);

        myField.value = myField.value.substring(0, startPos + startValue.length + (endPos - startPos))
            + endValue
            + myField.value.substring(startPos + startValue.length + (endPos - startPos), myField.value.length);


    } else {
        myField.value += startValue;
    }
}




function HideLoadingDiv() {
    console.log('HideLoadingDiv called');
    reloadOff(LoadingDiv);
}

//--- מניעה של שליחת טופס בלחיצה על כפתור אנטר בשדות
$(document).on("keypress", ":input:not(textarea)", function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
    }
});

function runSearch(e, pageUrl) {
    if (e.keyCode == 13) {
        window.location.href = pageUrl + '?Q=' + document.getElementById('Q').value;
    }
}

//function MarkMenuCss(id) {
//    document.getElementById(id + "_Menu").className = "MenuLinkOn";
//}

function CheckEmail(email) {

    invalidChars = " /:,;"
    if (email == "") {
        return false
    }
    for (i = 0; i < invalidChars.length; i++) {
        badChar = invalidChars.charAt(i)
        if (email.indexOf(badChar, 0) != -1) {
            return false
        }
    }
    atPos = email.indexOf("@", 1)
    if (atPos == -1) {
        return false
    }
    if (email.indexOf("@", atPos + 1) != -1) {
        return false
    }
    periodPos = email.indexOf(".", atPos)
    if (periodPos == -1) {
        return false
    }
    if (periodPos + 3 > email.length) {
        return false
    }
    return true
}

function createRequestObject() { var ro; if (window.XMLHttpRequest) { try { ro = new XMLHttpRequest(); } catch (e) { ro = false; } } else if (window.ActiveXObject) { try { ro = new ActiveXObject("Msxml2.HTMLHTTP"); } catch (e) { try { ro = new ActiveXObject("Microsoft.XMLHTTP"); } catch (e) { ro = false; } } } return ro; }

function MyencodeURIComponent(str) {

    var UsersStr = str.replace("'", "");

    return encodeURIComponent(UsersStr);
}

function reload(c){
	var flag = 0;
	if (c.style.display == "") flag = 1; 
		c.style.display="none";
	if (flag!=1) c.style.display = "";
}

function reload2(c, c2){
		c.style.display="";
		c2.style.display="none";
}

function reloadOn(c){
		c.style.display="";
}
function reloadOff(c) {
		c.style.display="none";
}

function reloadOnById(id) {
    document.getElementById(id).style.display = "";
}
function reloadOffById(id) {
    document.getElementById(id).style.display = "none";
}


function openwin() {

    var window_width = 300;
    var window_height = 135;
    browse = window.open('word.htm','browse','resizable=no,status=no,scrollbars=no,menubar=no,width=' + window_width + ',height=' + window_height + ',top=' + window_top + ',left=' + window_left + '');

    var window_top = 0;
    var window_left = 0;
    browse.focus();
}
function numbersonly(myfield, e, dec) {
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
        return false;
    }
    else
        return false;
}