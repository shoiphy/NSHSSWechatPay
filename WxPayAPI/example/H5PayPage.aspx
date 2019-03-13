<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H5PayPage.aspx.cs" Inherits="WxPayAPI.example.H5PayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form runat="server">
        <div style="margin-left:2%;">Total fee：</div><br/>
        <asp:TextBox ID="total_fee" runat="server" style="width:96%;height:35px;margin-left:2%;" value="1"/><br /><br />
        <div style="margin-left:2%;">Order no in database：</div><br/>
        <asp:TextBox ID="out_trade_no" runat="server" style="width:96%;height:35px;margin-left:2%;"/><br /><br />
        <div style="margin-left:2%;">Product Info：</div><br/>
        <asp:TextBox ID="body" runat="server" style="width:96%;height:35px;margin-left:2%;"/><br /><br />
        <div style="margin-left:2%;">Product attachment：</div><br/>
        <asp:TextBox ID="attach" runat="server" style="width:96%;height:35px;margin-left:2%;"/><br /><br />
        <br /><br />
       	<div align="center">
			<asp:Button ID="submit" runat="server" Text="支付" style="width:210px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;" OnClick="submit_Click" />
		</div>
	</form>
</body>
</html>
