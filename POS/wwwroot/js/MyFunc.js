window.showAlert = (message) => {
    console.log("Message from Blazor: " + message);
};

//function saveAsFile(filename, bytesBase64) {

//    var link = document.createElement('a');
//    link.download = filename;
//    link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + bytesBase64;
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
//}

function saveAsFile(filename, data) {
    const blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet:base64' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
}