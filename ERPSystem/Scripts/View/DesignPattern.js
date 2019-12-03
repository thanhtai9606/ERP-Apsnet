// A car "class"
function Car(model)
{
    this.model = model;
    this.color = "red";
    this.year = "2015";

    this.getInfo = function () {
        return this.model + " " + this.year;
    };
}
var myCar = new Car("abc");
console.log(myCar);
var log = (function () {
    var l = "";
    return {
        add: function (msg) { l += msg + "\n"; },
        show: function () { alert(l); l = ""; }
    }
})();



var HTMLChanger = (function () {
    var content = "abc";

    var changeHTML = function () {
        var a = "f";   
       
    } 
    return {
        callHTML: function () {
            changeHTML();
            console.log(content);
        }
    }

})();

HTMLChanger.callHTML();

