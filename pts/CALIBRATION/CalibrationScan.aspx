<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalibrationScan.aspx.cs" Inherits="PTSCOATING.pts.CALIBRATION.CalibrationScan" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <style>
        .utterances {
            position: relative;
            box-sizing: border-box;
            width: 100%;
            max-width: 760px;
            margin-left: auto;
            margin-right: auto;
        }
        .utterances-frame {
            color-scheme: light;
            position: absolute;
            left: 0;
            right: 0;
            width: 1px;
            min-width: 100%;
            max-width: 100%;
            height: 100%;
            border: 0;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="../../css/usercss/css/main.css">
    <link rel="stylesheet" type="text/css" href="../../css/usercss/css/dynamic.css">
    <link rel="preload" href="../../SCAN_files/main.css" as="style">
    <link rel="stylesheet" href="../../SCAN_files/main.css">
    <link rel="preload" href="../../SCAN_files/custom.css" as="style">
    <link rel="stylesheet" href="../../SCAN_files/custom.css">
    <link rel="stylesheet" href="../../SCAN_files/sharebar.css">

  
  <%--<link rel="alternate" type="application/rss+xml" title="Minhaz's Blog" href="https://blog.minhazav.dev/feed.xml">
    <link rel="shortcut icon" href="https://blog.minhazav.dev/assets/favicon.ico">
    <link rel="icon" type="image/png" sizes="32x32" href="https://blog.minhazav.dev/assets/favicon.ico">
    <link rel="canonical" href="https://blog.minhazav.dev/research/html5-qrcode">--%>


</head>
<body data-instant-allow-query-string="" data-instant-allow-external-links="">
  <header class="site-header"   style="display:none">
   
 </header>
      <header class="app-header">
              <%-- <a class="app-header__logo"  href="../homepage.aspx" > <image style="width:213px; height:80px"    src="https://apps.jindalsaw.com/claim/Jindal_Logo.jpg"></image> </a>
           &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp; &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b align="center" style="background-color:#009688;   width:900px ;font-family:Arial; font-size:49px">ACID WASH <%=Session["StationType"].ToString()%> SCANNER</b>--%>
            <a class="app-header__logo" style="background-color:#009688!important;"  href="../homepage.aspx"> 
             <img  style="width:80px; height:80px;" src="../../assets/Jindal_Logo.jpg"/>   </a> 
             <b class="app-menu__label" style="color:#ffffff!important;text-align:center!important;line-height:2.1!important;flex: 5 1 auto!important;font-size:35px;">CALIBRATION <%=Session["StationType"].ToString()%> SCANNER</b>
          <%--  <a class="app-header__logo"  href="/pts/homepage.aspx" style="background-color:#009688;   width:900px ;font-family:Arial; font-size:49px">Jindal BarCode</a>--%>

        </header>
   <main class="default-content" aria-label="Content">
     <div class="post-sidebar-right" style="position: fixed;">


  <%-- <div class="sidebar-ad-1">
        <script async="" src="SCAN_files/adsbygoogle_002.js" crossorigin="anonymous"></script>
        <ins class="adsbygoogle" style="display:block; text-align:center;" data-ad-layout="in-article" data-ad-format="fluid" data-ad-client="ca-pub-2583590292295592" data-ad-slot="6849341899"></ins>
        <script>
            (adsbygoogle = window.adsbygoogle || []).push({});
        </script>
   </div>--%>
</div>
     <div class="wrapper-content">
  <%--    <link rel="canonical" href="https://blog.minhazav.dev/research/html5-qrcode">--%>
<style>
#reader {
    width: 640px;
}
@media(max-width: 600px) {
                #reader {
                                width: 300px;
                }
}
.empty {
    display: block;
    width: 100%;
    height: 20px;
}
.alert {
    padding: 15px;
    margin-bottom: 20px;
    border: 1px solid transparent;
    border-radius: 4px;
}
.alert-info {
    color: #31708f;
    background-color: #d9edf7;
    border-color: #bce8f1;
}
.alert-success {
    color: #3c763d;
    background-color: #dff0d8;
    border-color: #d6e9c6;
}
#scanapp_ad {
    display: none;
}
@media(max-width: 1500px) {
                #scanapp_ad {
                                display: block;
                }
}
</style>
<link rel="stylesheet" href="../../SCAN_files/default.css">
<a href="homepage.aspx">Home</a>
<div class="container">
    <br />
    <div class="row">
        <div class="col-md-12" style="text-align: center;margin-bottom: 20px;">
            <div id="reader" style="display: inline-block; position: relative; padding: 0px; border: 1px solid silver;"><div style="text-align: left; margin: 0px;"><div style="position: absolute; top: 10px; right: 10px; z-index: 2; display: none; padding: 5pt; border: 1px solid silver; font-size: 10pt; background: rgb(248, 248, 248) none repeat scroll 0% 0%;"></div><img src="data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCA0NjAgNDYwIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA0NjAgNDYwIiB4bWw6c3BhY2U9InByZXNlcnZlIj48cGF0aCBkPSJNMjMwIDBDMTAyLjk3NSAwIDAgMTAyLjk3NSAwIDIzMHMxMDIuOTc1IDIzMCAyMzAgMjMwIDIzMC0xMDIuOTc0IDIzMC0yMzBTMzU3LjAyNSAwIDIzMCAwem0zOC4zMzMgMzc3LjM2YzAgOC42NzYtNy4wMzQgMTUuNzEtMTUuNzEgMTUuNzFoLTQzLjEwMWMtOC42NzYgMC0xNS43MS03LjAzNC0xNS43MS0xNS43MVYyMDIuNDc3YzAtOC42NzYgNy4wMzMtMTUuNzEgMTUuNzEtMTUuNzFoNDMuMTAxYzguNjc2IDAgMTUuNzEgNy4wMzMgMTUuNzEgMTUuNzFWMzc3LjM2ek0yMzAgMTU3Yy0yMS41MzkgMC0zOS0xNy40NjEtMzktMzlzMTcuNDYxLTM5IDM5LTM5IDM5IDE3LjQ2MSAzOSAzOS0xNy40NjEgMzktMzkgMzl6Ii8+PC9zdmc+" style="position: absolute; top: 4px; right: 4px; opacity: 0.6; cursor: pointer; z-index: 2; width: 16px; height: 16px;"><div id="reader__header_message" style="display: none; text-align: center; font-size: 14px; padding: 2px 10px; margin: 4px; border-top: 1px solid rgb(246, 246, 246); background: rgba(0, 0, 0, 0) none repeat scroll 0% 0%; color: rgb(17, 17, 17);">Requesting camera permissions...</div></div><div id="reader__scan_region" style="width: 100%; min-height: 100px; text-align: center; position: relative;"><video style="width: 640px;" muted="true"></video><canvas style="width: 250px; height: 250px; display: none;" id="qr-canvas" width="250" height="250"></canvas><div style="position: absolute; border-color: rgba(0, 0, 0, 0.48); border-style: solid; border-width: 115px 195px; box-sizing: border-box; inset: 0px;" id="qr-shaded-region"><div style="position: absolute; background-color: rgb(255, 255, 255); width: 40px; height: 5px; top: -5px; left: 0px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 40px; height: 5px; top: -5px; right: 0px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 40px; height: 5px; top: 255px; left: 0px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 40px; height: 5px; top: 255px; right: 0px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 5px; height: 45px; top: -5px; left: -5px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 5px; height: 45px; top: 215px; left: -5px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 5px; height: 45px; top: -5px; right: -5px;"></div><div style="position: absolute; background-color: rgb(255, 255, 255); width: 5px; height: 45px; top: 215px; right: -5px;"></div></div><div style="display: none; position: absolute; top: 0px; z-index: 1; background: yellow none repeat scroll 0% 0%; text-align: center; width: 100%;">Scanner paused</div></div><div id="reader__dashboard" style="width: 100%;"><div id="reader__dashboard_section" style="width: 100%; padding: 10px 0px; text-align: left;"><div><div id="reader__dashboard_section_csr" style="display: block; text-align: center;"><span style="margin-right: 10px;"><select style="display: none;" id="reader__camera_selection" disabled="disabled"><option value="xApzS2dowobLVkvwregPRdq0+I580UGU2pHFmGlTCeU=" selected="selected">Integrated Webcam</option></select></span><span><button style="opacity: 1; display: none;">Start Scanning</button><button style="display: inline-block;">Stop Scanning</button></span></div><div id="reader__dashboard_section_fsr" style="text-align: center; display: none;"><input id="reader__filescan_input" accept="image/*" type="file" style="width: 200px;" disabled="disabled"><span> Select Image</span></div></div><div style="text-align: center;"><a style="text-decoration: underline; display: none;" id="reader__dashboard_section_swaplink" href="#scan-using-file">Scan an Image File</a></div></div></div></div>
            <div class="empty"></div>
            <div id="scanned-result"></div>
        </div>
    </div>
</div>
         <div class="container">
             <div class="row">
                 <div class="col-md-12" style="text-align: center; margin-bottom: 20px;">
                     <div class="col-md-12">
                         <input type="button" id="btnsave" style="font-size:36px; width: 400px" class="btn btn-primary" value="Add Scan Batch No" />
                     </div>
                     <br />
                     <br />
                     <div class="col-md-12">
                         <a href="NewBatchList.aspx" style="font-size: 36px; width: 400px" class="btn btn-primary">Add Manual Batch No</a>
                     </div>
                      <br />
                     <br />
                     <div class="col-md-12">
                         <a href="TakePicture.aspx" style="font-size: 36px; width: 400px" class="btn btn-primary">Take Picture</a>
                     </div>
                     
                     <br />
                     <br />
                     <div class="col-md-12">
                         <a href="AcidScan.aspx" style="font-size: 36px; width: 400px" class="btn btn-danger">Reset</a>
                     </div>
                     <br />
                     <br />
                     <div class="col-md-12">
                         <a href="../homepage.aspx" style="font-size: 36px; width: 400px" class="btn btn-dark">Back</a>
                     </div>
                 </div>
             </div>
         </div>
 
<%--<script>
    (adsbygoogle = window.adsbygoogle || []).push({});
</script>--%>
<script src="../../SCAN_files/highlight.js"></script>
<script src="../../SCAN_files/html5-qrcode.js"></script>
<script>
function docReady(fn) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
        // call on next available tick
        setTimeout(fn, 1);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
}
/** Ugly function to write the results to a table dynamically. */
    function printScanResultPretty(codeId, decodedText, decodedResult) {

     



                let resultSection = document.getElementById('scanned-result');
    let tableBodyId = "scanned-result-table-body";
    if (!document.getElementById(tableBodyId)) {
        let table = document.createElement("table");
        table.className = "styled-table";
        table.style.width = "100%";
        resultSection.appendChild(table);
        let theader = document.createElement('thead');
        let trow = document.createElement('tr');
        let th1 = document.createElement('td');
        th1.innerText = "Count";
        let th2 = document.createElement('td');
        th2.innerText = "Format";
        let th3 = document.createElement('td');
        th3.innerText = "Result";
        trow.appendChild(th1);
        trow.appendChild(th2);
        trow.appendChild(th3);
        theader.appendChild(trow);
        table.appendChild(theader);
        let tbody = document.createElement("tbody");
        tbody.id = tableBodyId;
        table.appendChild(tbody);
    }
        let tbody = document.getElementById(tableBodyId);
        let trow = document.createElement('tr');
        let td1 = document.createElement('td');
        td1.innerText = `${codeId}`;
        let td2 = document.createElement('td');
        td2.innerText = `${decodedResult.result.format.formatName}`;
        let td3 = document.createElement('td');
        td3.innerText = `${decodedText}`;


        trow.appendChild(td1);
        trow.appendChild(td2);
        trow.appendChild(td3);
        tbody.appendChild(trow);

  


  

   
   
}
docReady(function() {
                hljs.initHighlightingOnLoad();
                var lastMessage;
                var codeId = 1;
    function onScanSuccess(decodedText, decodedResult) {

        if (codeId > 1) {
            alert('You can not scan more than one barcode!!')
            return;
        }
        /**
         * If you following the code example of this page by looking at the
         * source code of the demo page - good job!!
         * 
         * Tip: update this function with a success callback of your choise.
         */
                                if (lastMessage !== decodedText) {
                                                lastMessage = decodedText;
                        printScanResultPretty(codeId, decodedText, decodedResult);
                        ++codeId;
                                }
                }
                let html5QrcodeScanner = new Html5QrcodeScanner(
        "reader", 
        { 
            fps: 10,
            qrbox: { width: 300, height: 300 },
            // Important notice: this is experimental feature, use it at your
            // own risk. See documentation in
            // mebjas@/html5-qrcode/src/experimental-features.ts
            experimentalFeatures: {
                useBarCodeDetectorIfSupported: true
            },
            rememberLastUsedCamera: true
        });
                html5QrcodeScanner.render(onScanSuccess);
});
</script>
     </div>
   </main>

    <script async="" src="../../SCAN_files/js"></script>
 
    
    <script src="../../SCAN_files/jquery.min.js"></script>
    <script src="../../SCAN_files/json2.js"></script>
     <script type="text/javascript">
      

         $("#btnsave").click(function () {

            
          
             var customers = new Array();
             $("#scanned-result-table-body tr").each(function () {
                 var row = $(this);
                 var customer = {};
                 customer.counts = row.find("td").eq(0).html();
                 customer.types = row.find("td").eq(1).html();
                 customer.code = row.find("td").eq(2).html().replace('<br>','');
                 customers.push(customer);
             });

             if (customers.length>1)
             {
                     alert("Please cannot scan more the one value");
                     return;
                 
             }


             if (customers.length > 0) {
                  $.ajax({
                     type: "POST",
                      url: "AcidScan.aspx/SaveUser",
                     data: '{customers: ' + JSON.stringify(customers) + '}',

                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (r) {
                         window.location.href = "NewBatchList.aspx";
                         // alert(r + " record(s) inserted.");
                     }
                 });
             }
             else {
                 alert("Please scan qr code");
                 return;
             }
            
         });
     </script>
    

</body></html>
