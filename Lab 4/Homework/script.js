function show_image(image_path)
{
    let image_preview = document.getElementById("image_preview");
    image_preview.innerHTML = "";
    image = document.createElement("img");
    image.setAttribute('src', image_path);
    image.setAttribute('width', '600');
    image_preview.appendChild(image);
}


function hide_image()
{
    let image_preview = document.getElementById("image_preview");
    image_preview.innerHTML = "";
}