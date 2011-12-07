
function checkCurrentPageSetting() {

    $(".mainDiv").tabs();
    setUpMenuForPage();


}

function setUpMenuForPage() {

    $(document).ready(function() {
        $(".menuDrop li a").click(function() {
            var hidden = $(this).parents("li").children("ul").is(":hidden");
            $(".menuDrop>ul>li>ul").hide()
            $(".menuDrop>ul>li>a").removeClass();

            if (hidden) {
                $(this)
						.parents("li").children("ul").toggle()
						.parents("li").children("a").addClass("zoneCur");
            }
        });
    });

    $(document).click(function(event) {
        if (!$(event.target).parents("li").children("a").hasClass('zoneCur')) {
            $(".menuDrop>ul>li>ul").hide();
            $(".menuDrop>ul>li>a").removeClass();
        }
    });


}

function saySomething(varsd)
{
    alert(varsd);
    return;

}