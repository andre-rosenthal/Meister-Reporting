<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="MeisterReporting.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 712px;
        }
        .auto-style11 {
            height: 25px;
            width: 477px;
        }
        .auto-style12 {
            height: 25px;
            width: 478px;
            text-align: left;
        }
        .auto-style13 {
            width: 477px;
        }
        .auto-style14 {
            width: 478px;
            text-align: left;
        }
        .auto-style15 {
            width: 291px;
        }
    </style>
</head>
<body style="width: 958px; height: 418px">
    <form id="form1" runat="server">
                    <table cellpadding="4" cellspacing="0" style="border-collapse:collapse;">
                        <tr>
                            <td class="auto-style15">
                                <table cellpadding="0" class="auto-style2">
                                    <tr>
                                        <td align="center" colspan="2" style="color:White;background-color:#507CD1;font-size:0.9em;font-weight:bold;">Log In</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Id:</asp:Label>
                                        </td>
                                        <td class="auto-style12">
                                            <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" OnTextChanged="UserName_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                        </td>
                                        <td class="auto-style14">
                                            <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password" OnTextChanged="Password_TextChanged" style="height: 19px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13">
                                            <asp:Label ID="SAPClientLabel" runat="server" AssociatedControlID="SAPClient">Client #:</asp:Label>
                                        </td>
                                        <td class="auto-style14">
                                            <asp:TextBox ID="SAPClient" runat="server" Font-Size="0.8em" TextMode="Number" OnTextChanged="SAPClient_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Password" ErrorMessage="Client is required." ToolTip="Client is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="auto-style13">
                                            <asp:Label ID="Label2" runat="server" AssociatedControlID="SAPGateway">Gateway:</asp:Label>
                                        </td>
                                        <td class="auto-style14">
                                            <asp:TextBox ID="SAPGateway" runat="server" Font-Size="0.8em" TextMode="Url" OnTextChanged="SAPGateway_TextChanged" Width="800px" Height="25px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="SAPGateway" ErrorMessage="SAP Gateway URL is required." ToolTip="SAP Gateway URL is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Language:&nbsp;&nbsp;
                                            <br />
&nbsp;<asp:DropDownList ID="LoginLanguage" runat="server" Width="277px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CheckBox ID="OD4Mode" runat="server" Text="Gateway is running OData V4" OnCheckedChanged="OD4Mode_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" Text="Log In" ValidationGroup="Login1" OnClick="LoginButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <p>
                        &nbsp;</p>
    </form>
</body>
</html>
