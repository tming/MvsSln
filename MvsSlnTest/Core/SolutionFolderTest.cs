﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.r_eg.MvsSln.Core;
using net.r_eg.MvsSln.Extensions;

namespace net.r_eg.MvsSlnTest.Core
{
    [TestClass]
    public class SolutionFolderTest
    {
        [TestMethod]
        public void SubFolderTest1()
        {
            var f0  = new SolutionFolder("dir3", "hMSBuild.bat");
            var f1  = new SolutionFolder("dir2", f0);
            var f2  = new SolutionFolder("dir1", f1);

            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "dir1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = f2.header.pGuid,
                        path        = f2.header.path,
                        fullPath    = f2.header.fullPath,

                        parent = new SolutionFolder()
                        {
                            header = new ProjectItem()
                            {
                                name        = "dir2",
                                EpType      = ProjectType.SlnFolder,
                                pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                                pGuid       = f1.header.pGuid,
                                path        = f1.header.path,
                                fullPath    = f1.header.fullPath,

                                parent = new SolutionFolder()
                                {
                                    header = new ProjectItem()
                                    {
                                        name        = "dir3",
                                        EpType      = ProjectType.SlnFolder,
                                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                                        pGuid       = f0.header.pGuid,
                                        path        = f0.header.path,
                                        fullPath    = f0.header.fullPath,

                                        parent = null,
                                    },
                                },
                            },
                        },
                    },
                },

                f2
            );
        }

        [TestMethod]
        public void CtorTest1()
        {
            var f = new SolutionFolder("MyFolder1", ".gnt\\gnt.core", ".gnt\\packages.config");
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core", ".gnt\\packages.config"
                    }
                },
                f
            );
            Assert.AreEqual(2, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
            Assert.AreEqual((RawText)".gnt\\packages.config", f.items.ElementAt(1));
            

            f = new SolutionFolder("MyFolder1", ".gnt\\gnt.core");
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));


            f = new SolutionFolder("MyFolder1");
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },
                    items = new List<RawText>() { }
                },
                f
            );
            Assert.AreEqual(0, f.items.Count());
        }

        [TestMethod]
        public void CtorTest2()
        {
            var f = new SolutionFolder("MyFolder4", (new RawText[] { ".gnt\\gnt.core" }).AsEnumerable());

            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder4",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );

            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
        }

        [TestMethod]
        public void CtorTest3()
        {
            var f = new SolutionFolder
            (
                "MyFolder1", 
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", 
                new SolutionFolder("dir1"), 
                ".gnt\\gnt.core", 
                ".gnt\\packages.config"
            );

            Assert.AreEqual
            (
                new SolutionFolder(f),
                f
            );
        }

        [TestMethod]
        public void CtorTest4()
        {
            var f0  = new SolutionFolder("dir1");
            var f   = new SolutionFolder("MyFolder1", f0, ".gnt\\gnt.core", ".gnt\\packages.config");

            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        parent      = f0,
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core", ".gnt\\packages.config"
                    }
                },
                f
            );
            Assert.AreEqual(2, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
            Assert.AreEqual((RawText)".gnt\\packages.config", f.items.ElementAt(1));


            f = new SolutionFolder("MyFolder2", f0, ".gnt\\gnt.core");
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder2",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        parent      = f0,
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));


            f = new SolutionFolder("MyFolder3", f0);
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder3",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        parent      = f0,
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },
                    items = new List<RawText>() { }
                },
                f
            );
            Assert.AreEqual(0, f.items.Count());


            f = new SolutionFolder("MyFolder4", f0, (new RawText[] { ".gnt\\gnt.core" }).AsEnumerable());
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder4",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        parent      = f0,
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
        }

        [TestMethod]
        public void CtorTest5()
        {
            var f = new SolutionFolder(
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder1", 
                new RawText[] { ".gnt\\gnt.core", ".gnt\\packages.config" }
            );
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core", ".gnt\\packages.config"
                    }
                },
                f
            );
            Assert.AreEqual(2, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
            Assert.AreEqual((RawText)".gnt\\packages.config", f.items.ElementAt(1));


            var f0 = new SolutionFolder();

            f = new SolutionFolder
            (
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", 
                "MyFolder1", 
                f0, 
                ".gnt\\gnt.core", ".gnt\\packages.config"
            );
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        path        = f.header.path,
                        parent      = f0,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core", ".gnt\\packages.config"
                    }
                },
                f
            );
            Assert.AreEqual(2, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
            Assert.AreEqual((RawText)".gnt\\packages.config", f.items.ElementAt(1));
        }

        [TestMethod]
        public void CtorTest6()
        {
            var f = new SolutionFolder
            (
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder1", 
                new RawText[] { ".gnt\\gnt.core" }
            );
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));


            f = new SolutionFolder("{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder1", new RawText[] { });
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },
                    items = new List<RawText>() { }
                },
                f
            );
            Assert.AreEqual(0, f.items.Count());


            f = new SolutionFolder
            (
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder1", 
                (new RawText[] { ".gnt\\gnt.core" }).AsEnumerable()
            );
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
        }

        [TestMethod]
        public void CtorTest7()
        {
            var f0 = new SolutionFolder("dir1");

            var f = new SolutionFolder
            (
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder4", f0, 
                (new RawText[] { ".gnt\\gnt.core" }).AsEnumerable()
            );
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder4",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        parent      = f0,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));


            f = new SolutionFolder
            (
                "{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder1", f0, 
                ".gnt\\gnt.core", ".gnt\\packages.config"
            );
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        parent      = f0,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core", ".gnt\\packages.config"
                    }
                },
                f
            );
            Assert.AreEqual(2, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));
            Assert.AreEqual((RawText)".gnt\\packages.config", f.items.ElementAt(1));


            f = new SolutionFolder("{EE7DD6B7-56F4-478D-8745-3D204D915473}", "MyFolder2", f0, ".gnt\\gnt.core");
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder2",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{EE7DD6B7-56F4-478D-8745-3D204D915473}",
                        parent      = f0,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },

                    items = new List<RawText>()
                    {
                        ".gnt\\gnt.core"
                    }
                },
                f
            );
            Assert.AreEqual(1, f.items.Count());
            Assert.AreEqual((RawText)".gnt\\gnt.core", f.items.ElementAt(0));


            f = new SolutionFolder("{5D5C7878-22BE-4E5B-BD96-6CBBAC614AD3}", "MyFolder3", f0);
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "MyFolder3",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{5D5C7878-22BE-4E5B-BD96-6CBBAC614AD3}",
                        parent      = f0,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },
                    items = new List<RawText>() { }
                },
                f
            );
            Assert.AreEqual(0, f.items.Count());
        }

        [TestMethod]
        public void CtorTest8()
        {
            Guid guid = new Guid("{8B05CE1C-999E-460D-80FF-E44DFEC019E7}");

            var f = new SolutionFolder(guid, "dir1");
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "dir1",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = f.header.pGuid,
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },
                    items = new List<RawText>() { }
                },
                f
            );
            Assert.AreEqual(0, f.items.Count());


            f = new SolutionFolder("{8B05CE1C-999E-460D-80FF-E44DFEC019E7}", "dir2", null);
            Assert.AreEqual
            (
                new SolutionFolder()
                {
                    header = new ProjectItem()
                    {
                        name        = "dir2",
                        EpType      = ProjectType.SlnFolder,
                        pType       = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}",
                        pGuid       = "{8B05CE1C-999E-460D-80FF-E44DFEC019E7}",
                        path        = f.header.path,
                        fullPath    = f.header.fullPath,
                    },
                    items = new List<RawText>() { }
                },
                f
            );
            Assert.AreEqual(0, f.items.Count());
        }
    }
}
