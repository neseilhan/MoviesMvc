function ucgenDoldur(karakter,satirsayisi,ters = false) {
    var sonuc;
    var sonucArray = new Array(); //uzunluk belirtmeye gerek yok dinamik.

    for (var i = 0; i < satirsayisi; i++) {
        sonuc = "";
        for (var j = 0; j <= i; j++) {
            sonuc += karakter;
        }
        sonucArray[i] = sonuc;
    }
    sonuc = "";
    if (!ters) {
        var k = 0;
        while (k < sonucArray.length) {
            sonuc += sonucArray[k] + "\n";
            k++;
        }
      
    } else {
        var k = sonucArray.length - 1;
        do {
            sonuc += sonucArray[k] + "\n";
            k--;
        } while(k>=0);
    }
    return sonuc;
}
    function onLoadUcgenDoldur(karakter,satirsayisi) {
        var textarea = document.getElementById('mytextareaid');
        var sonuc = ucgenDoldur(karakter, satirsayisi);
        textarea.value = sonuc;
}
var ters = true;

function onClickUcgenDoldur(karakter, satirsayisi) {
    var textarea = document.getElementsByName("mytextarea")[0]; //alternatif bir koleksiyondur
    //var textarea = document.getElementById('mytextareaid'); 
    var sonuc = ucgenDoldur(karakter, satirsayisi, ters);
    textarea.value = sonuc;
    var body = document.getElementsByTagName('body')[0];
    if (ters)
        body.style.backgroundColor = "black";
    else
        body.style.backgroundColor = "pink";
    ters = !ters; //degeri tersine çevirdik true false
}

