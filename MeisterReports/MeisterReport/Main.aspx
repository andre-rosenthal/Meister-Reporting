<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="MeisterReporting.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <script lang="javascript" type="text/javascript">
            function Hourglass() {
                document.body.style.cursor = 'wait';
            }
            function Default() {
                document.body.style.cursor = 'default';
            }
        </script>
        <h1>Meister Reporter</h1>
        <h2>
            <asp:Label ID="UrlConn" runat="server" Font-Bold="True" Font-Italic="True" Text="&gt;&gt;&gt; url &lt;&lt;&lt;&lt;&lt;"></asp:Label>
        </h2>
        <h2>Select an option:</h2>
        <br />
        <br />
        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Show Schedule" CssClass="auto-style1" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Show my Reports" OnClick="Button3_Click" CssClass="auto-style1" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="Run a Report" OnClick="Button4_Click" CssClass="auto-style1" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button10" runat="server" CssClass="auto-style1" OnClick="Button10_Click" Text="Logoff" Width="127px" />
        <br />
        <br />
        <div id="Grid3" runat="server">
            <br />
            <asp:GridView ID="GridView3" runat="server" AutoGenerateSelectButton="True" Caption="Reports found for user"
                AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanged="GridView3_PageIndexChanged"
                OnPageIndexChanging="GridView3_PageIndexChanging" OnSelectedIndexChanged="GridView3_SelectedIndexChanged"
                OnRowDeleting="GridView3_RowDeleting" OnRowDataBound="GridView3_RowDataBound" Style="font-size: large" CaptionAlign="Left">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div id="SearchSAP" runat="server">
                    <h4>
                        <asp:Label ID="Label1" runat="server" Style="font-size: medium" Text="Enter a t-code or report hint"></asp:Label>
                    </h4>
                    <asp:TextBox ID="TextBox1" runat="server" ToolTip="Try RFAUSZ00" CssClass="auto-style1"></asp:TextBox>

                    <asp:DropDownList ID="ddpDemo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddpDemo_SelectedIndexChanged" CssClass="auto-style1">
                        <asp:ListItem>RM07RESLH</asp:ListItem>
                        <asp:ListItem>S_ALR_87012291</asp:ListItem>
                        <asp:ListItem>S_ALR_87012332</asp:ListItem>
                        <asp:ListItem>S_ALR_87012326</asp:ListItem>
                        <asp:ListItem>SD_SALES_ORDERS_VIEW</asp:ListItem>
                    </asp:DropDownList>

                    <asp:Button ID="Button1" runat="server" Text="Search SAP" Unload="Default()" OnClientClick="Hourglass()" OnClick="Button1_Click" Style="font-size: medium" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox8" runat="server" Visible="False"></asp:TextBox>
                    <br />
                    <asp:Label ID="SearchLbl" runat="server"></asp:Label>
                </div>
                <div id="Grid1" runat="server">
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateSelectButton="True" CellPadding="4" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        ShowFooter="True" ShowHeaderWhenEmpty="True" Width="677px" Caption="Reports found at SAP matching the Hint passed "
                        OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333" GridLines="None" Style="font-size: medium" OnPreRender="GridView1_PreRender" OnUnload="GridView1_Unload" CaptionAlign="Left">
                        
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
                <p></p>
                <div id="Grid4" runat="server">
                    <asp:Label ID="Label10" runat="server" Text="Select Variant DEMO ..." Style="font-size: medium"></asp:Label>
                    <p></p>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateSelectButton="True" Caption="Variants found for chosen report"
                        CellPadding="4" ForeColor="#333333" GridLines="None"  OnSelectedIndexChanged="GridView4_SelectedIndexChanged" Style="font-size: medium" CaptionAlign="Left">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
                <br />
                <br />

                <div id="Grid2" runat="server">
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" CellPadding="4"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowFooter="True" ShowHeaderWhenEmpty="True"
                        Width="830px" AutoGenerateEditButton="True" OnRowCancelingEdit="GridView2_RowCancelingEdit"
                        OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" Caption="Parameters Found for report"
                        ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView2_PageIndexChanging"
                        OnRowDataBound="GridView2_RowDataBound" OnRowUpdated="GridView2_RowUpdated" Style="font-size: medium" CaptionAlign="Left">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <br />
                </div>
                <div id="VariantSave" runat="server">
                    <h3>
                        <asp:Label ID="Foo" runat="server">Please provide a name and description for this Variant</asp:Label>
                    </h3>
                    <h4>
                        <asp:Label ID="Label7" runat="server" Text="Variant Name:"></asp:Label>
                        &nbsp;<asp:TextBox ID="TextBox4" runat="server" AutoPostBack="True" OnTextChanged="TextBox4_TextChanged" Width="243px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label8" runat="server" Text="Description:"></asp:Label>
                        &nbsp;<asp:TextBox ID="TextBox5" runat="server" AutoPostBack="True" OnTextChanged="TextBox5_TextChanged" Width="245px"></asp:TextBox>
                    </h4>
                    &nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:Button ID="Button6" runat="server" Text="Save as New Variant" OnClick="Button6_Click" Enabled="False" Style="font-size: medium" />

                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Override" Style="font-size: medium" />

                </div>
                <br />
                <div id="BeforeB2" runat="server">
                    <h4>
                        <asp:Label ID="Label3" runat="server" Text="Selections for Response:"></asp:Label>
                        &nbsp; </h4>

                    <br />
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Style="font-size: medium">
                        <asp:ListItem Value="M" Selected="True">Return EDM</asp:ListItem>
                        <asp:ListItem Value="N">Named Columns</asp:ListItem>
                    </asp:RadioButtonList>

                    <h4>
                        <br />
                        Recurrence:<br />
                        <br />
                    </h4>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" Style="font-size: medium">
                        <asp:ListItem Selected="True" Value="I">Immediate</asp:ListItem>
                        <asp:ListItem Value="T">This Date</asp:ListItem>
                        <asp:ListItem Value="D">Daily</asp:ListItem>
                        <asp:ListItem Value="W">Weekly</asp:ListItem>
                        <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                        <asp:ListItem Value="S">Semi-Annually</asp:ListItem>
                        <asp:ListItem Value="A">Annually</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Panel ID="Cal" runat="server">
                        Select a Date:<br />
                        &nbsp;<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" SelectedDate="<%# DateTime.Today %>" OnDayRender="Calendar1_DayRender" />

                    </asp:Panel>
                    <br />
                    <asp:Panel ID="DOWs" runat="server">
                        <asp:Label ID="lbDOW" runat="server"></asp:Label>
                        <br />
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" Style="font-size: medium">
                            <asp:ListItem Value="S">Sunday</asp:ListItem>
                            <asp:ListItem Value="M">Monday</asp:ListItem>
                            <asp:ListItem Value="T">Tuesday</asp:ListItem>
                            <asp:ListItem Value="W">Wednesday</asp:ListItem>
                            <asp:ListItem Value="R">Thursday</asp:ListItem>
                            <asp:ListItem Value="F">Friday</asp:ListItem>
                            <asp:ListItem Value="X">Saturday</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="Hours" runat="server">
                        <asp:Label ID="LbHou" runat="server"></asp:Label>
                        <span class="auto-style1">Hour Slot:</span>&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox7" runat="server" CausesValidation="True" MaxLength="2" Width="33px"
                            OnTextChanged="TextBox7_TextChanged" ToolTip="00-23 as Hour" AutoPostBack="True" Style="font-size: medium"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label9" runat="server" Text="Nickname:" Style="font-size: medium"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtNickName" runat="server" Width="375px" Style="font-size: medium" OnTextChanged="txtNickName_TextChanged"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Create Item" Style="font-size: medium" />
                        &nbsp;&nbsp;
                        <asp:Button ID="Button9" runat="server" OnClick="ConfirmDelete_Click" Style="font-size: medium" Text="Delete Item" Visible="False" />
                        &nbsp;
                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox2_CheckedChanged" Style="font-size: medium" Text="Confirm Deletion?" Visible="False" />
                        <br />
                    </asp:Panel>
                    <p>
                        <asp:Button ID="Button2" runat="server" OnClientClick="Hourglass()" Text="Execute" OnClick="Button2_Click" Style="font-size: medium" />
                    </p>
                </div>
                <div id="AfterB2" runat="server">
                    <h4>
                        <asp:Label ID="Label4" runat="server" Text="Report Unique Identifier"></asp:Label>
                        <br />
                        <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" ReadOnly="True" Width="279px"></asp:TextBox>
                    </h4>
                </div>
                <p>
                    <hr />
                    <h3>
                        <asp:Label ID="msgLabel" runat="server" Text="SAP Messages .... "></asp:Label></h3>
                    <asp:TextBox ID="TextBox3" runat="server" Height="45px" Width="749px" BorderStyle="None"
                        AutoPostBack="True"></asp:TextBox>
                    <p>
                    </p>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="GridView3" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel">
            <ProgressTemplate>
                Processing Request ...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
   


</html>
