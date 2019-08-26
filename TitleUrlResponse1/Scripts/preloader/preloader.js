$(document).ready(function () {
    var loaderDown = $("#preloaderDownLoad");
    var myform = $("#myform");

    loaderDown.on("click", function () {
       
      document.getElementById('myform').hidden = true;
        
        $(".dws-progress-bar").circularProgress({
            color: "red",
            line_width: 18,
            height: "350px",
            width: "350px",
            percent: 0,
            counter_clockwise: false,
            starting_position: 50
        }).circularProgress('animate', 100, 2500);

    });

});