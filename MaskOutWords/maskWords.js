function maskText() {
           var words = ["yesterday", "Dog", "food", "walk"];
           var text = $("#txtInput").val();
           var maskedText = maskOutWords(words, text);
           $("#lblResult").html(maskedText);
       }

function maskOutWords(words, text) {
    for (var index in words) {
        var word = words[index].toLowerCase();
		var startIndex =  text.toLowerCase().indexOf(word);
        while (startIndex > -1) { 
            var lenght = word.length;
            var endIndex = startIndex + lenght;
            var astericsStr = "";
            for (var i = 0; i < lenght; i++) {
                astericsStr += "*";
            }
            var newText = text.substring(0, startIndex) + astericsStr + text.substring(endIndex);
            text = newText;
			startIndex =  text.toLowerCase().indexOf(word); // in case we have the word more than once
        }
    }
    return text;
}