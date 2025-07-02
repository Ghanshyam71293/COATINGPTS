<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TakePicture.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.TakePicture" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1. Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" type="text/css" href="../../css/usercss/css/main.css">
  <%--  <link rel="stylesheet" type="text/css" href="../../css/usercss/css/dynamic.css">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="https://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.js" type="text/javascript"></script>  

      <!-- this javascript plugin uses the webcam.swf file to capture image and send the image to server for further processing -->
    <script type="text/javascript">
        var canvas, context, timer;
        var constraints = window.constraints = {
            audio: false,
            video: { facingMode: "environment" }, // change to "user" for front camera, see https://developer.mozilla.org/en-US/docs/Web/API/MediaTrackConstraints/facingMode


        };
        //  (HTML5 based camera only) this portion of code will be used when browser supports navigator.getUserMedia  *********     */
        window.addEventListener("DOMContentLoaded", function () {
            canvas = document.getElementById("canvasU"),
                context = canvas.getContext("2d"),
                video = document.getElementById("video"),
                videoObj = { "video": true },
                errBack = function (error) {
                    console.log("Video capture error: ", error.code);
                };

            // check if we can use HTML5 based camera (through mediaDevices.getUserMedia() function)
            if (navigator.mediaDevices.getUserMedia) { // Standard browser
                // display HTML5 camera
                document.getElementById("userMedia").style.display = '';
                // adding click event to take photo from webcam
                document.getElementById("snap").addEventListener("click", function () {
                    context.drawImage(video, 0, 0, 640, 480);
                });

                navigator.mediaDevices
                    .getUserMedia(constraints)
                    .then(handleSuccess)
                    .catch(handleError);
            }
            // check if we can use HTML5 based camera (through .getUserMedia() function in Webkit based browser)
            else if (navigator.webkitGetUserMedia) { // WebKit-prefixed for Google Chrome
                // display HTML5 camera
                document.getElementById("userMedia").style.display = '';
                // adding click event to take photo from webcam
                document.getElementById("snap").addEventListener("click", function () {
                    context.drawImage(video, 0, 0, 640, 480);
                });
                navigator.webkitGetUserMedia(videoObj, function (stream) {
                    video.src = window.webkitURL.createObjectURL(stream);
                    video.play();
                }, errBack);
            }
            // check if we can use HTML5 based camera (through .getUserMedia() function in Firefox based browser)
            else if (navigator.mozGetUserMedia) { // moz-prefixed for Firefox 
                // display HTML5 camera
                document.getElementById("userMedia").style.display = '';
                // adding click event to take photo from webcam
                document.getElementById("snap").addEventListener("click", function () {
                    context.drawImage(video, 0, 0, 640, 480);
                });
                navigator.mozGetUserMedia(videoObj, function (stream) {
                    video.mozSrcObject = window.webkitURL.createObjectURL(stream);
                    video.play();
                }, errBack);
            }
            else
            // if we can not use any of HTML5 based camera ways then we use Flash based camera
            {
                // display Flash camera
                document.getElementById("flashDiv").style.display = '';
                document.getElementById("flashOut").innerHTML = (webcam.get_html(640, 480));
            }

        }, false);

        // (all type of camera) set the default selection of barcode type
        var selection = "All barcodes (slow)";

        // (all type of camera) gets the selection text of "barcode type to scan" combobox
        function selectType(type) {
            selection = type.options[type.selectedIndex].text;
        }

        function handleSuccess(stream) {
            var video = document.querySelector('video');
            var videoTracks = stream.getVideoTracks();
            console.log('Got stream with constraints:', constraints);
            console.log(`Using video device: ${videoTracks[0].label}`);
            window.stream = stream; // make variable available to browser console
            video.srcObject = stream;
        }

        function handleError(error) {
            if (error.name === 'ConstraintNotSatisfiedError') {
                var v = constraints.video;
                errorMsg(`The resolution ${v.width.exact}x${v.height.exact} px is not supported by your device.`);
            } else if (error.name === 'PermissionDeniedError') {
                errorMsg('Permissions have not been granted to use your camera and ' +
                    'microphone, you need to allow the page access to your devices in ' +
                    'order for the demo to work.');
            }
            errorMsg(`getUserMedia error: ${error.name}`, error);
        }

        function errorMsg(msg, error) {
            var errorElement = document.querySelector('#errorMsg');
            errorElement.innerHTML += `<p>${msg}</p>`;
            if (typeof error !== 'undefined') {
                console.error(error);
            }
        }

        // (HTML5 based camera only)
        // uploads the image to the server 
        function UploadToCloud() {
           /* document.getElementById('report').innerHTML = "scanning the current frame......";*/
            context.drawImage(video, 0, 0, 640, 480);
            $("#upload").attr('disabled', 'disabled');
            $("#upload").attr("value", "Uploading...");
            var img = canvas.toDataURL('image/jpeg', 0.9).split(',')[1];
            // send AJAX request to the server with image data 
            alert('Picture Captured!!')
            $.ajax({
                url: "TakePicture.aspx/Upload",
                type: "POST",
                data: "{ 'image': '" + img + "' , 'type': '" + selection + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                // on success output the result returned by the server side (See HTML5Camera.aspx)
                success: function (data, status) {
                    /*debugger;*/
                    $("#upload").removeAttr('disabled');
                    $("#upload").attr("value", "Upload");
                    if (data.d.length != 0) {
                        if (data.d == "Error") {

                            alert("Api Not Working!!");
                          /*  document.getElementById('Error').innerHTML = "Api Not Working!!"*/

                        }
                        else {
                            document.getElementById('OutListBoxHTML5').value = "";
                            var htmlSelect = document.getElementById('OutListBoxHTML5');

                            htmlSelect.value = data.d + "\r\n" + htmlSelect.value;
                        }
                    }
                },
         
                error: function (data) {
                    //document.getElementById('report').innerHTML = "no barcode found, scanning.....";
                    $("#upload").removeAttr('disabled');
                    $("#upload").attr("value", "Upload");
                },
                async: false
            });
           /* timer = setTimeout(UploadToCloud, 1000);*/  // will capture new image to detect barcode after 3000 mili second
        }
      
       

      
    </script>

     <style>
    span
    {
        font-size:20px;
    }
    </style>
</head>
<body>
     <header class="app-header">
            <%-- <a class="app-header__logo"  href="../homepage.aspx" > <image style="width:213px; height:80px"    src="https://apps.jindalsaw.com/claim/Jindal_Logo.jpg"></image> </a>
           &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b align="center" style="background-color:#009688;   width:900px ;font-family:Arial; font-size:49px">Final Inspection <%=Session["StationType"].ToString()%> Camera</b>--%>
           <a class="app-header__logo" style="background-color:#009688!important;"  href="../homepage.aspx"> 
             <img  style="width:80px; height:80px;" src="../../assets/Jindal_Logo.jpg""/>   </a> 
             <b class="app-menu__label" style="color:#ffffff!important;text-align:center!important;line-height:2.1!important;flex: 5 1 auto!important;font-size:35px;">FINAL <%=Session["StationType"].ToString()%> CAMERA</b>
         <%--   <a class="app-header__logo"  href="/pts/homepage.aspx" style="background-color:#009688;   width:900px ;font-family:Arial; font-size:49px">Jindal BarCode</a>--%>

        </header>
    <form id="form1" runat="server">
        <br />
        <br />
        <div id="userMedia" style="display: block; height: 575px; width: 1182px">
            <table>
                <tr>
                    <td valign="top">
                        &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;    &nbsp;&nbsp;   &nbsp;&nbsp;  <video id="video" width="640" height="70%" autoplay playsinline muted></video>
                        <canvas id="canvasU" width="640" height="480" style="display: none"></canvas>
                    </td>
                </tr>
                <tr>
                     <td>
                  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;         <input id="snap" class="btn btn-primary" style="width:400px; font-size: 36px;" type="button" onclick="UploadToCloud();" value="Capture" />
                         </td>
                    </tr>
                 <tr valign="top">
                      <td ></td>
                    <td valign="top">
                     
                        <h4 style="color:red; display:none"   id="Error"/>
                        </td>
                     </tr>
                <tr valign="top">
                    <td valign="top">
                      
                        &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input style="width: 600px;  height: 100px; font-size:48px;"    id="OutListBoxHTML5"  > 
                         </input>
                         
                       
                        
                        
                        <h4 id="report"/>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                  &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;    &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; <input type="button" id="btnsave" style="font-size: 36px; width: 400px" class="btn btn-primary" value="Add" />
                        </td>
                </tr>
                <tr>
                     <td valign="top">
                          &nbsp;&nbsp; 
                         </td>
                    </tr>
                 <tr>
                    <td valign="top">
               &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;     &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; <a href="TakePicture.aspx" style="font-size: 36px; width: 400px" class="btn btn-danger">Reset</a>
                        </td>
                </tr>
                <tr>
                     <td valign="top">
                          &nbsp;&nbsp; 
                         </td>
                    </tr>
                 <tr>
                    <td valign="top">
               &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;     &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; <a href="FinalScan.aspx" style="font-size: 36px; width: 400px" class="btn btn-dark">Back</a>
                        </td>
                </tr>
                 <script src="https://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.js" type="text/javascript"></script>  
                <script type="text/javascript">
                    $("#btnsave").click(function () {
                        var bla = $('#OutListBoxHTML5').val().replace(" ","");
                        
                        if (bla!="")
                        {
                            debugger;
                            $.ajax({
                                type: "POST",
                                url: "TakePicture.aspx/SaveUser",
                                data: '{customers: ' + JSON.stringify(bla) + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (r) {
                                    window.location.href = "NewBatchList.aspx";

                                }
                            });

                        }
                        else
                        {
                            debugger;
                            alert("Please take picture");
                            return;
                        }
                    });
                </script>

            </table>
        </div>
       
    <div id="upload_results" style="background-color:#eee;"></div>
     
    <div id="flashOut"> </div>
              
    </form>
       <script type="text/javascript" src="webcam.js"></script>

     
   
</body>
</html>
