$(document).ready(function() {
    $("#menu").css({
        "width":"100%",
        "float":"left"
    })
    $("#menu ul").css({
        "list-style": "none",
        "margin": "0",
        "padding": "0",
        "width": "11em",
        "float": "left"
    })
    $("#menu a, #menu h2").css({
        "font": "bold 11px/16px arial, helvetica, sans-serif",
        "display": "block",
        "border-width": "1px",
        "border-style": "solid",
        "border-color": "#d0d0d0 #808080 #505050 #b0b0b0",
        "margin": "0",
        "padding": "2px 3px"
    })
    $("#menu ul li ul").slideUp( 0, function() {});
    $("#menu ul li").click(
        (event) => {
            menu = $(event.currentTarget).find("ul")

            if(!menu.is(':visible'))
            {
                $("#menu ul li ul").slideUp( "slow", function() {});
                menu.slideDown( "slow", function() {});
            }
            else
            {
                menu.slideUp( "slow", function() {});
            }
        }
        )

})