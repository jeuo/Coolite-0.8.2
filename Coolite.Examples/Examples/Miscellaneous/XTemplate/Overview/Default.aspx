﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Coolite Toolkit Example - XTemplate</title>    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />    
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Parent> persons = new List<Parent>(3);

            for (int i = 0; i < 3; i++)
            {
                Parent person = new Parent();
                person.Name = "Parent" + i;
                person.Title = "Title" + i;
                person.Company = "Company" + i;
                person.Email = "person" + i + "@company.com";
                person.Address = "Address" + i;
                person.City = "City";
                person.State = "State" + i;
                person.Zip = "123456";
                person.Drinks = new string[] { "Juice", "Tea", "Coffee" };
                person.Children = new List<Child>(3);

                for (int j = 0; j < 3; j++)
                {
                    person.Children.Add(new Child("Name" + j, j));
                    
                }
                
                persons.Add(person);
            }

           ObjHolder1.Items.Add("persons", persons);
        }
        
        public class Child
        {
            public Child() { }

            public Child(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Parent
        {
            public string Name { get; set; }
            public string Title { get; set; }
            public string Company { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string[] Drinks { get; set; }
            public List<Child> Children { get; set; }

            [Newtonsoft.Json.JsonIgnore]
            public object ThisPropertyWillNotBeSerialized
            {
                get
                {
                    return new object();
                }
            }
        }
    </script>
    
    <style type="text/css">
        .x-panel-body p {
            margin: 5px;
            font-size: 11px;
        }
        
        .even {
    	    background-color: gray;
    	    color: #fff;
        }
        
        .odd {
    	    background-color: #fff;
    	    color: #000;
        }
    </style>
    
    <script type="text/javascript">
         var isFemale = function (name){
             return name == 'Name1';
         }
         
         var isBaby = function (age){
            return age < 1;
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />

        <h1>XTemplate</h1>      
        
        <ext:ObjectHolder ID="ObjHolder1" runat="server" />
        
        <h3>1. Auto filling of arrays and scope switching</h3>
         
        <p>
           Using the tpl tag and the for operator, you can switch to the scope of the object
           specified by for and access its members to populate the template. If the variable in for is an array,
           it will auto-fill, repeating the template block inside the tpl tag for each item in the array:
        </p>
        
        <ext:XTemplate ID="Tpl1" runat="server">   
            <tpl for=".">
                <p>
                    <b>Name:</b> {Name}<br/>
                    <b>Title:</b> {Title}<br/>
                    <b>Company:</b> {Company}
                </p>
                
                <p>
                    <span style="font-weight:bold; margin-right:-29px;">Children:</span>
                    <tpl for="Children">
                       <span style="margin-left:30px;">{Name}</span><br />                                                   
                    </tpl>
                </p>                                
                
                <hr />                                
            </tpl>     
        </ext:XTemplate>
        
        <ext:Panel ID="Panel1" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl1}.overwrite(this.body, #{ObjHolder1}.persons);" />
            </Listeners>
        </ext:Panel>
        
        <br />
        <br />
        <br />
        
        <h3>2. Access to parent object from within sub-template scope</h3>
         
        <p>
           When processing a sub-template, for example while looping through a child array, you can access the parent object's members via the parent object:
        </p>
        
        <ext:XTemplate ID="Tpl2" runat="server">   
            <p>Name: {Name}</p>
            <p>
                Children:<br />
                <tpl for="Children">
                    <tpl if="Age &gt; 0"> <%--<-- Note that the > is encoded--%>
                        <span style="margin-left:30px;">{Name}</span>
                        <span style="margin-left:10px;">Father: {parent.Name}</span><br />
                    </tpl>
                </tpl>
            </p>
        </ext:XTemplate>
        
        <ext:Panel ID="Panel2" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl2}.overwrite(this.body, #{ObjHolder1}.persons[0]);" />
            </Listeners>
        </ext:Panel>
        
        <br />
        <br />
        <br />
        
        <h3>3. Array item index and basic math support </h3>
         
        <p>
           While processing an array, the special variable {#} will provide the current array index + 1 (starts at 1, not 0). Templates also support the basic math operators + - * and / that can be applied directly on numeric data values:
        </p>
        
        <ext:XTemplate ID="Tpl3" runat="server">   
            <p>Name: {Name}</p>
            <div>
                Children: <br />
                <tpl for="Children">
                    <div style="margin-left:30px;">
                        <tpl if="Age &gt; 0">  <%--<-- Note that the > is encoded--%>
                            <p style="font-weight:bold;">{#}: {Name}</p>  <%--<-- Auto-number each item--%>
                            <p>In 5 Years: {Age+5}</p>  <%--<-- Basic math--%>
                            <p>Father: {parent.Name}</p>
                        </tpl>
                    </div>
                </tpl>
            </div>
        </ext:XTemplate>
        
        <ext:Panel ID="Panel3" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl3}.overwrite(this.body, #{ObjHolder1}.persons[0]);" />
            </Listeners>
        </ext:Panel>
        
         <br />
        <br />
        <br />
        
        <h3>4. Auto-rendering of flat arrays </h3>
         
        <p>
           Flat arrays that contain values (and not objects) can be auto-rendered using the special {.} variable inside a loop. This variable will represent the value of the array at the current index:
        </p>
        
        <ext:XTemplate ID="Tpl4" runat="server">   
            <p>{Name}'s favorite beverages:</p>
            <tpl for="Drinks">
               <div> - {.}</div>
            </tpl>
        </ext:XTemplate>
        
        <ext:Panel ID="Panel4" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl4}.overwrite(this.body, #{ObjHolder1}.persons[0]);" />
            </Listeners>
        </ext:Panel>
        
         <br />
        <br />
        <br />
        
        <h3>5. Basic conditional logic </h3>
         
        <p>
           Using the tpl tag and the if operator you can provide conditional checks for deciding whether or not to render specific parts of the template. Note that there is no else operator — if needed, you should use two opposite if statements. Properly-encoded attributes are required as seen in the following example:
        </p>
        
        <ext:XTemplate ID="Tpl5" runat="server">   
            <p>Name: {Name}</p>
            <div><b>Children:</b>
                <tpl for="Children">
                    <tpl if="Age &gt; 0">  <%--<-- Note that the > is encoded--%>
                        <p>{Name}</p>
                    </tpl>
                </tpl>
            </div>
        </ext:XTemplate>
        
        <ext:Panel ID="Panel5" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl5}.overwrite(this.body, #{ObjHolder1}.persons[0]);" />
            </Listeners>
        </ext:Panel>
        
         <br />
        <br />
        <br />
        
        <h3>6. Ability to execute arbitrary inline code </h3>
         
        <div>
           In an XTemplate, anything between {[ ... ]} is considered code to be executed in the scope of the template. There are some special variables available in that code:

           <p><b>values</b>: The values in the current scope. If you are using scope changing sub-templates, you can change what values is.</p>
           <p><b>parent</b>: The scope (values) of the ancestor template.</p>
           <p><b>xindex</b>: If you are in a looping template, the index of the loop you are in (1-based).</p>
           <p><b>xcount</b>: If you are in a looping template, the total length of the array you are looping.</p>
           <p><b>fm</b>: An alias for Ext.util.Format.</p>
           <p>This example demonstrates basic row striping using an inline code block and the xindex variable:</p>
        </div>
        
        <ext:XTemplate ID="Tpl6" runat="server">   
            <tpl for=".">
                <div class="{[xindex % 2 === 0 ? "odd" : "even"]}">
                    <p>Name: {Name}</p>
                    <p>Company: {[values.Company.toUpperCase() + ", " + values.Title]}</p>
                    <p>Children:
                        <tpl for="Children">                           
                            {Name}                           
                        </tpl>
                    </p>
                    <tpl if="[xcount - xindex] &gt; 0">
                        <hr />
                    </tpl>
                </div>
            </tpl>
        </ext:XTemplate>
        
        <ext:Panel ID="Panel6" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl6}.overwrite(this.body, #{ObjHolder1}.persons);" />
            </Listeners>
        </ext:Panel>
        
         <br />
        <br />
        <br />
        
        <h3>7. Template functions  </h3>
         
        <p>
           One or more functions can be used into the XTemplate for more complex processing:
        </p>
        
        <ext:XTemplate ID="Tpl7" runat="server">   
            <p>Name: {Name}</p>
            <div>Children:
                <tpl for="Children">
                    <tpl if="isFemale(Name)">
                        <p>Girl: {Name} - {Age}</p>
                    </tpl>
                    <tpl if="isFemale(Name) == false">
                        <p>Boy: {Name} - {Age}</p>
                    </tpl>
                    <tpl if="isBaby(Age)">
                        <p>{Name} is a baby!</p>
                    </tpl>
                </tpl>
            </div>
        </ext:XTemplate>
        
        <ext:Panel ID="Panel7" runat="server" BodyStyle="background-color:#F8F8F8; padding:5px; border:dotted 1px gray;">
            <Listeners>
                <Render Handler="#{Tpl7}.overwrite(this.body, #{ObjHolder1}.persons[0]);" />
            </Listeners>
        </ext:Panel>
        
    </form>
</body>
</html>
