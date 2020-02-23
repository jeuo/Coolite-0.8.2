﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Coolite.Ext.Web.TreePanel tree = new Coolite.Ext.Web.TreePanel();

        this.PlaceHolder1.Controls.Add(tree);

        tree.ID = "TreePanel1";
        tree.Width = Unit.Pixel(300);
        tree.Height = Unit.Pixel(450);
        tree.Icon = Icon.BookOpen;
        tree.Title = "Catalog";
        tree.AutoScroll = true;

        Coolite.Ext.Web.Button btnExpand = new Coolite.Ext.Web.Button();
        btnExpand.Text = "Expand All";
        btnExpand.Listeners.Click.Handler = tree.ClientID + ".expandAll();";

        Coolite.Ext.Web.Button btnCollapse = new Coolite.Ext.Web.Button();
        btnCollapse.Text = "Collapse All";
        btnCollapse.Listeners.Click.Handler = tree.ClientID + ".collapseAll();";

        Toolbar toolBar = new Toolbar();
        toolBar.ID = "Toolbar1";
        toolBar.Items.Add(btnExpand);
        toolBar.Items.Add(btnCollapse);
        tree.TopBar.Add(toolBar);

        StatusBar statusBar = new StatusBar();
        statusBar.AutoClear = 1000;
        tree.BottomBar.Add(statusBar);

        tree.Listeners.Click.Handler = statusBar.ClientID + ".setStatus({text: 'Node Selected: <b>' + node.text + '</b>', clear: true});";
        tree.Listeners.ExpandNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + node.text + '</b>', clear: true});";
        tree.Listeners.ExpandNode.Delay = 30;
        tree.Listeners.CollapseNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + node.text + '</b>', clear: true});";
        
        Coolite.Ext.Web.TreeNode root = new Coolite.Ext.Web.TreeNode("Composers");
        root.Expanded = true;
        tree.Root.Add(root);

        List<Composer> composers = this.GetData();

        foreach (Composer composer in composers)
        {
            Coolite.Ext.Web.TreeNode composerNode = new Coolite.Ext.Web.TreeNode(composer.Name, Icon.UserGray);
            root.Nodes.Add(composerNode);
            
            foreach(Composition composition in composer.Compositions)
            {
                Coolite.Ext.Web.TreeNode compositionNode = new Coolite.Ext.Web.TreeNode(composition.Type.ToString());
                composerNode.Nodes.Add(compositionNode);

                foreach (Piece piece in composition.Pieces)
                {
                    Coolite.Ext.Web.TreeNode pieceNode = new Coolite.Ext.Web.TreeNode(piece.Title, Icon.Music);
                    compositionNode.Nodes.Add(pieceNode);
                }
            }
        } 
    }
    
    public class Composer
    {
        public Composer(string name) { this.Name = name; }
        public string Name { get; set; }
        
        private List<Composition> compositions;
        public List<Composition> Compositions
        { 
            get
            {
                if(this.compositions == null)
                {
                    this.compositions = new List<Composition>();
                }
                return this.compositions;
            } 
        }
    }

    public class Composition
    {
        public Composition() { }

        public Composition(CompositionType type)
        {
            this.Type = type;
        }
        
        public CompositionType Type { get; set; }

        private List<Piece> pieces;
        public List<Piece> Pieces
        {
            get
            {
                if (this.pieces == null)
                {
                    this.pieces = new List<Piece>();
                }
                return this.pieces;
            }
        } 
    }
        
    public class Piece
    {
        public Piece(){}

        public Piece(string title) 
        { 
            this.Title = title; 
        }
        
        public string Title { get; set; }
    }

    public enum CompositionType
    {
        Concertos,
        Quartets,
        Sonatas,
        Symphonies
    }

    public List<Composer> GetData()
    { 
        Composer beethoven = new Composer("Beethoven");

        Composition beethovenConcertos = new Composition(CompositionType.Concertos);
        Composition beethovenQuartets = new Composition(CompositionType.Quartets);
        Composition beethovenSonatas = new Composition(CompositionType.Sonatas);
        Composition beethovenSymphonies = new Composition(CompositionType.Symphonies);

        beethovenConcertos.Pieces.AddRange(new List<Piece> { 
            new Piece{ Title = "No. 1 - C" },
            new Piece{ Title = "No. 2 - B-Flat Major" },
            new Piece{ Title = "No. 3 - C Minor" },
            new Piece{ Title = "No. 4 - G Major" },
            new Piece{ Title = "No. 5 - E-Flat Major" }
        });

        beethovenQuartets.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Six String Quartets" },
            new Piece{ Title = "Three String Quartets" },
            new Piece{ Title = "Grosse Fugue for String Quartets" }
        });

        beethovenSonatas.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Sonata in A Minor" },
            new Piece{ Title = "sonata in F Major" }
        });

        beethovenSymphonies.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "No. 1 - C Major" },
            new Piece{ Title = "No. 2 - D Major" },
            new Piece{ Title = "No. 3 - E-Flat Major" },
            new Piece{ Title = "No. 4 - B-Flat Major" },
            new Piece{ Title = "No. 5 - C Minor" },
            new Piece{ Title = "No. 6 - F Major" },
            new Piece{ Title = "No. 7 - A Major" },
            new Piece{ Title = "No. 8 - F Major" },
            new Piece{ Title = "No. 9 - D Minor" }
        });

        beethoven.Compositions.AddRange(new List<Composition>{
            beethovenConcertos, 
            beethovenQuartets,
            beethovenSonatas,
            beethovenSymphonies 
        });


        Composer brahms = new Composer("Brahms");

        Composition brahmsConcertos = new Composition(CompositionType.Concertos);
        Composition brahmsQuartets = new Composition(CompositionType.Quartets);
        Composition brahmsSonatas = new Composition(CompositionType.Sonatas);
        Composition brahmsSymphonies = new Composition(CompositionType.Symphonies);

        brahmsConcertos.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Violin Concerto" },
            new Piece{ Title = "Double Concerto - A Minor" },
            new Piece{ Title = "Piano Concerto No. 1 - D Minor" },
            new Piece{ Title = "Piano Concerto No. 2 - B-Flat Major" }
        });

        brahmsQuartets.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Piano Quartet No. 1 - G Minor" },
            new Piece{ Title = "Piano Quartet No. 2 - A Major" },
            new Piece{ Title = "Piano Quartet No. 3 - C Minor" },
            new Piece{ Title = "Piano Quartet No. 3 - B-Flat Minor" }
        });

        brahmsSonatas.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Two Sonatas for Clarinet - F Minor" },
            new Piece{ Title = "Two Sonatas for Clarinet - E-Flat Major" }
        });

        brahmsSymphonies.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "No. 1 - C Minor" },
            new Piece{ Title = "No. 2 - D Minor" },
            new Piece{ Title = "No. 3 - F Major" },
            new Piece{ Title = "No. 4 - E Minor" }
        });

        brahms.Compositions.AddRange(new List<Composition>{
            brahmsConcertos, 
            brahmsQuartets,
            brahmsSonatas,
            brahmsSymphonies 
        });
        
        
        Composer mozart = new Composer("Mozart");

        Composition mozartConcertos = new Composition(CompositionType.Concertos);

        mozartConcertos.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Piano Concerto No. 12" },
            new Piece{ Title = "Piano Concerto No. 17" },
            new Piece{ Title = "Clarinet Concerto" },
            new Piece{ Title = "Violin Concerto No. 5" },
            new Piece{ Title = "Violin Concerto No. 4" }
        });

        mozart.Compositions.Add(mozartConcertos);

        return new List<Composer>{ beethoven, brahms, mozart };
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Multi Node TreePanel built from code-behind - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Multi Node TreePanel built from code-behind</h1>

        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    </form>
</body>
</html>