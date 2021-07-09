<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <script src="jquery.js"></script>
    <script>
        $(document).ready(() => {
            console.log("OK");
            $.ajax({
                type: "GET",
                url: "/urls",
                success: function(dataJSON, status) {
                    data = JSON.parse(dataJSON);
                    $("#topUrl").html("");
                    data.forEach(function(url) {
                        $("#topUrl").append(`<div style="border:1px solid black;">
                          <h4>${url.url}</h4>
                          <h4>${url.clicks}</h4>
                          </div>`);
                        let newButton = $(document.createElement('button')).prop({
                            type: 'button',
                            innerHTML: 'Press me',
                            class: 'btn-styled'
                        });
                        newButton.click(function() {
                            $.ajax({
                                type: "POST",
                                url: "/urls",
                                data: url.url
                            });
                        });
                        $("#topUrl").append(newButton);
                    });
                }
                }
            );
        });
    </script>
    <title>URL TOP SPOT</title>
</head>
<body>
Login
<form action="/login" method="post">
    <input name="username">
    <input name="password" type="password">
    <button type="submit">Submit</button>
</form>
<h1>TOP 10 URLS</h1>
<div id="topUrl"></div>
</body>
</html>
