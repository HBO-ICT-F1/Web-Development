const recordHidden = $("#record_hidden")[0];
const recordField = $("#record_field")[0];
const recordBox = $("#record-box")[0];
let record = "";

$(window).resize(positionDiv)
$(recordField).ready(positionDiv);
$(recordField).keyup(function() {
    if (recordHidden.value !== "") {
        recordField.style.borderColor = "red";
        recordHidden.value = "";
    }
    if (record === recordField.value) {
        return;
    }
    record = recordField.value
    $.ajax({
        type: "post",
        url: "/product/lookup",
        data: { lookup: record },
        success: handleRequest
    })
});

function handleRequest(response) {
    if (response === undefined || Object.keys(response).length === 0) {
        hideRecordBox();
        return;
    }
    let html = "";
    Object.keys(response).forEach(function (key) {
        const value = response[key];
        html += `<div class="row" data-id="${key}">${value}</div>\n`
    })
    recordBox.innerHTML = html;
    recordBox.style.display = "block";
    $("#record-box .row").click(onRowClicked)
}

function onRowClicked(e) {
    recordField.value = e.target.innerText
    recordHidden.value = e.target.dataset.id
    recordField.style.borderColor = "";
    hideRecordBox();
}

function hideRecordBox() {
    recordBox.style.display = "none";
    recordBox.innerHTML = "";
}

function positionDiv() {
    recordBox.style.top = recordField.offsetTop + recordField.offsetHeight + "px";
    recordBox.style.left = recordField.offsetLeft + "px";
    recordBox.style.width = recordField.scrollWidth + 2 + "px";
}