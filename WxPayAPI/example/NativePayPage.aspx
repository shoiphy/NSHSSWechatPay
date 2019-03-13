<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NativePayPage.aspx.cs" Inherits="WxPayAPI.NativePayPage" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html;image/gif;charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <title>Wechat pay-QRCODE</title>
</head>
<body>
	<div style="margin-left: 10px;color:#00CD00;font-size:30px;font-weight: bolder;">QRCODE Mode 1</div><br/>
	<asp:Image ID="Image1" runat="server" style="width:200px;height:200px;"/>
	<br/><br/><br/>
	<div style="margin-left: 10px;color:#00CD00;font-size:30px;font-weight: bolder;">QRCODE Mode 2</div><br/>
	<asp:Image ID="Image2" runat="server" style="width:200px;height:200px;"/>
	
</body>
</html>
