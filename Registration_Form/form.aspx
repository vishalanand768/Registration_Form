<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form.aspx.cs" Inherits="Registration_Form.form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type ="text/javascript">
        function validation() {
            var dabba = "";
            dabba += checkfname();
            dabba += checklname();
          
            if (dabba == "") {
                return true;
            }
            else {
                alert(dabba);
                return false;
            }
        }
        function checkfname() {
            var TB = document.getElementById('txtfname');
            if (TB.value == "") {
                return 'Plz Enter your first name !!\n';
            }

            else {
                return "";
            }
        }
        function checklname() {
            var Tb = document.getElementById('txtlname');
            if (Tb.value == "") {
                return 'Plz Enter your Last name!!\n';
            }
            else {
                return "";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>First Name  : </td>
                <td><asp:TextBox ID="txtfname" runat="server"></asp:TextBox></td>
            </tr>

             <tr>
                <td>Last Name  : </td>
                <td><asp:TextBox ID="txtlname" runat="server"></asp:TextBox></td>
            </tr>

             <tr>
                <td>Gender  : </td>
                <td><asp:RadioButtonList ID="rblgen" runat="server" RepeatColumns="3">
                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>

             <tr>
                <td>Country  : </td>
                <td><asp:DropDownList ID="ddlcountry" runat="server"></asp:DropDownList></td>
            </tr>

             <tr>
                <td>Qualification  : </td>
                <td><asp:DropDownList ID="ddlqual" runat="server"></asp:DropDownList></td>
            </tr>

             <tr>
                <td>Blood Group  : </td>
                <td><asp:DropDownList ID="ddlbldgp" runat="server"></asp:DropDownList></td>
            </tr>

             <tr>
                <td></td>
                <td><asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick=" return validation()" OnClick="btnsave_Click" ></asp:Button></td>
            </tr>

             <tr>
                <td></td>
                <td><asp:GridView ID="grd" runat="server" OnRowCommand="grd_RowCommand" AutoGenerateColumns="false">
                  <Columns>
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <%#Eval("Fisrt_Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Name">
                            <ItemTemplate>
                                <%#Eval("Last_Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gender">
                            <ItemTemplate>
                                 <%#Eval("Gender").ToString()=="1" ? "male" :Eval("gender").ToString()=="2" ? "female" : "others"  %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Country">
                            <ItemTemplate>
                                <%#Eval("cname") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qualification">
                            <ItemTemplate>
                                <%#Eval("qname") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Blood Group">
                            <ItemTemplate>
                                <%#Eval("bname") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName="A" CommandArgument='<%#Eval("id") %>' />
                            <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="B" CommandArgument='<%#Eval("id") %>' />
                       </ItemTemplate>
                       </asp:TemplateField>

                     </Columns>
                    </asp:GridView></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
