

if (document.title == "All Events") {
    var scrollBtn = document.querySelector("#scroll");

    scrollBtn.addEventListener("click", function () {
        var events = document.querySelector("#eventbox");
        events.scrollIntoView({
            behavior: "smooth",
            alignTop: true
        });
    });
    var searchInput = document.querySelector("#search-bar");
    searchInput.addEventListener("keyup", function (event) {

        var search = event.target.value.toLowerCase();
        var events = document.querySelectorAll(".event");
        if (search.length != 0) {
            document.querySelectorAll(".event-row").forEach(item => {
                item.setAttribute("style", "flex-direction: column; margin-bottom: 0;")
            });
            document.querySelectorAll(".event").forEach(item => {
                item.setAttribute("style", " margin-bottom: 1rem;")
            });
            // container.style.dislay = "block";
            events.forEach(item => {
                var title = item.querySelector(".eventTitle").textContent.toLowerCase();
                var location = item.querySelector("#location").textContent.toLowerCase();

                if (title.includes(search) || location.includes(search)) {
                    item.style.display = "block";
                }
                else {
                    item.style.display = "none";
                }

            })
        }
        else {
            events.forEach(item => {
                item.style.display = "block";
            });
            document.querySelectorAll(".event-row").forEach(item => {
                if (document.body.clientWidth > 1310) {
                    item.setAttribute("style", "flex-direction: row;")
                }
                else {
                    item.setAttribute("style", "flex-direction: column;")
                }
            });
            document.querySelectorAll(".event").forEach(item => {
                item.setAttribute("style", " margin-bottom: 0;")
            });
        }

    });

    var filter = document.querySelector("#category");
    filter.addEventListener("change", function () {
        var events = document.querySelectorAll(".event");
        var category = filter.value.toLowerCase();
        if (category != "all") {
            events.forEach(item => {
                var eventCategory = item.classList[1].toLowerCase();
                if (category == eventCategory) {
                    item.style.display = "block";
                }
                else {
                    item.style.display = "none";
                }
            });
        }
        else {
            events.forEach(item => {
                item.style.display = "block";
            });
        }
    });

}




var darkMode = document.querySelector("#dark-mode");

darkMode.addEventListener("click", function () {

    document.body.classList.toggle("dark");
    if (document.body.classList.contains("dark")) {
        document.querySelectorAll(".table").forEach(item => {
            item.classList.add("dark-mode");
        });
        if (document.title == "Home") {
            document.querySelector(".background").style.opacity = "0.9";
            document.querySelector(".text-on-background").style.color = "var(--primary-color)";
        }
        if (document.querySelector(".details-details") != null) {
            document.querySelector(".details-details").setAttribute("style", "background-color: #F8F6F0;");
            document.querySelectorAll(".desc-box-details").forEach(item => {
                item.setAttribute("style", "background-color: #F8F6F0;");
            });
        }
        document.querySelector("#dark-mode a").setAttribute("class", "fa-solid fa-sun");

        localStorage.darkMode = false;
    }

    else {
        document.querySelectorAll(".table").forEach(item => {
            item.classList.remove("dark-mode");
        });
        if (document.title == "Home") {
            document.querySelector(".background").style.opacity = "0.5";
            document.querySelector(".text-on-background").style.color = "var(--primary-color)";
        }
        document.querySelector("#dark-mode a").setAttribute("class", "fa-solid fa-moon");

        if (document.querySelector(".details-details") != null) {
            document.querySelector(".details-details").setAttribute("style", "background-color: #333; color: gainsboro;");
            document.querySelectorAll(".desc-box-details").forEach(item => {
                item.setAttribute("style", "background-color: #333; color: gainsboro;");
            });
        }
        localStorage.darkMode = true;
    }
}
);

var control = document.querySelector("#controlButton");
var button = document.querySelector(".buttons");
control.addEventListener("click", function () {
    if (button.classList.contains("active")) {
        button.classList.remove("active");
    }
    else {
        button.classList.add("active");
    }

});


var buttons = document.querySelectorAll(".control .buttons .control-row button");
buttons.forEach(item => {
    item.addEventListener("click", function () {
        document.documentElement.style.setProperty("--primary-color", item.style.backgroundColor);
        document.documentElement.style.setProperty("--secondary-color", item.style.color);
        localStorage.primaryColor = item.style.backgroundColor;
        localStorage.secondaryColor = item.style.color;
    })
});

if (localStorage.primaryColor == "skyblue") {
    document.documentElement.style.setProperty("--primary-color", "skyblue");
    document.documentElement.style.setProperty("--secondary-color", "dodgerblue");
}
else if (localStorage.primaryColor == "yellow") {
    document.documentElement.style.setProperty("--primary-color", "yellow");
    document.documentElement.style.setProperty("--secondary-color", "orange");
}
else if (localStorage.primaryColor == "rgb(255, 0, 78)") {
    document.documentElement.style.setProperty("--primary-color", "#ff004e");
    document.documentElement.style.setProperty("--secondary-color", "#ff8d00");
}
else if (localStorage.primaryColor == "yellowgreen") {
    document.documentElement.style.setProperty("--primary-color", "yellowgreen");
    document.documentElement.style.setProperty("--secondary-color", "yellow");
}
else if (localStorage.primaryColor == "orange") {
    document.documentElement.style.setProperty("--primary-color", "orange");
    document.documentElement.style.setProperty("--secondary-color", "yellowgreen");
}
else if (localStorage.primaryColor == "rgb(142, 0, 255)") {
    document.documentElement.style.setProperty("--primary-color", "#8e00ff");
    document.documentElement.style.setProperty("--secondary-color", "#ff00e8");
}



var fontIncre = document.querySelector("#font-incre");

fontIncre.addEventListener("click", function () {
    document.body.style.fontSize = "x-large";
    localStorage.fontSize = "x-large";
}
);

var fontDecre = document.querySelector("#font-decre");

fontDecre.addEventListener("click", function () {
    document.body.style.fontSize = "medium";
    localStorage.fontSize = "medium";
}
);

var fontReset = document.querySelector("#font-reset");
fontReset.addEventListener("click", function () {
    document.body.style.fontSize = "large";
    localStorage.fontSize = "large";
}
);

if (localStorage.darkMode == "false") {
    document.body.classList.add("dark");
    document.querySelectorAll(".table").forEach(item => {
        item.classList.add("dark-mode");
    });
    if (document.title == "Home") {
        document.querySelector(".background").style.opacity = "0.9";
        document.querySelector(".text-on-background").style.color = "var(--primary-color)";
    }
    
    if (document.querySelector(".details-details") != null) {
        document.querySelector(".details-details").setAttribute("style", "background-color: #F8F6F0;");
        document.querySelectorAll(".desc-box-details").forEach(item => {
            item.setAttribute("style", "background-color: #F8F6F0;");
        });
    }
    

    document.querySelector("#dark-mode a").setAttribute("class", "fa-solid fa-sun");
}
else {
    document.body.classList.remove("dark");
    document.querySelectorAll(".table").forEach(item => {
        item.classList.remove("dark-mode");
    });
    if (document.title == "Home") {
        document.querySelector(".background").style.opacity = "0.5";
        document.querySelector(".text-on-background").style.color = "var(--primary-color)";
    }

    if (document.querySelector(".details-details") != null) {
        document.querySelector(".details-details").setAttribute("style", "background-color: #333; color: gainsboro;");
        document.querySelectorAll(".desc-box-details").forEach(item => {
            item.setAttribute("style", "background-color: #333; color: gainsboro;");
        });
        document.querySelector("#dark-mode a").setAttribute("class", "fa-solid fa-moon");
    }
    
}

if (localStorage.fontSize == "x-large") {
    document.body.style.fontSize = "x-large";
}
else if (localStorage.fontSize == "medium") {
    document.body.style.fontSize = "medium";
}
else {
    document.body.style.fontSize = "large";
}




