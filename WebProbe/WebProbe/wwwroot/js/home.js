document.getElementById("button").addEventListener("click", function () {
    var tableContainer = document.getElementById("table-container");

    tableContainer.innerHTML = "";

    var passedNumber = parseInt(document.getElementById("number").value);

    if (isNaN(passedNumber) || passedNumber < 5 || passedNumber > 20) {
        var param = document.createElement("p");
        param.innerText = "Podana wartość jest nieprawidłowa, przyjęto n=5";
        param.classList.add("alert");
        tableContainer.appendChild(param);
        passedNumber = 5;
    }

    const numbers = [];
    for (var i = 0; i < passedNumber; i++) {
        numbers.push(Math.floor(Math.random() * 99) + 1);
    }

    var table = document.createElement("table");
    tableContainer.appendChild(table);


    var thead = document.createElement("thead");
    table.appendChild(thead);

    var tbody = document.createElement("tbody");
    table.appendChild(tbody);

    var headerRow = document.createElement("tr");
    thead.appendChild(headerRow);

    var emptyHeader = document.createElement("th");
    emptyHeader.classList.add("header");
    emptyHeader.textContent = "";
    headerRow.appendChild(emptyHeader);

    for (var i = 0; i < passedNumber; i++) {
        var colHeader = document.createElement("th");
        colHeader.classList.add("header");
        colHeader.textContent = numbers[i];
        headerRow.appendChild(colHeader);
    }

    for (var i = 0; i < passedNumber; i++) {
        var row = document.createElement("tr");
        tbody.appendChild(row);

        var rowHeader = document.createElement("th");
        rowHeader.classList.add("header");
        rowHeader.textContent = numbers[i];
        row.appendChild(rowHeader);

        for (var j = 0; j < passedNumber; j++) {
            var result = numbers[i] * numbers[j];
            var cell = document.createElement("td");
            cell.textContent = result;

            if (result % 3 === 0) {
                cell.classList.add("zero");
            } else if (result % 3 === 1) {
                cell.classList.add("one");
            }
            else {
                cell.classList.add("two");
            }

            row.appendChild(cell);


        }
    }
});