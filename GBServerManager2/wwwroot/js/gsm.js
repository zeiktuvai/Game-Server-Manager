function scrollToEnd(textarea) {
    textarea.scrollTop = textarea.scrollHeight;
    textarea.scrollToEnd();
}

function scrollEnd(elementName) {
    var element = document.getElementById(elementName);
    element.scrollTop = element.scrollHeight;
}
