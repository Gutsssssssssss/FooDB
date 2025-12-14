using FooDB;

namespace FooTest;

public class BlockStorageTest
{

    [Fact]
    public void TestBlockStoragePersistence()
    {
        using var ms = new MemoryStream();
        var storage = new BlockStorage(ms);
            
        using (var firstBlock = storage.CreateNew())
        using (var secondBlock = storage.CreateNew())
        using (var thirdBlock = storage.CreateNew())
        {
            Assert.Equal((uint)0, firstBlock.Id);
            Assert.Equal((uint)1, secondBlock.Id);
                
            secondBlock.SetHeader(1, 100);
            secondBlock.SetHeader(2, 200);
                
            Assert.Equal((uint)2, thirdBlock.Id);
            Assert.Equal(storage.BlockSize * 3, ms.Length);
        }
            
        var storage2 = new BlockStorage(ms);
        Assert.Equal((uint)0, storage2.Find(0).Id);
        Assert.Equal((uint)1, storage2.Find(1).Id);
        Assert.Equal((uint)2, storage2.Find(2).Id);
            
        Assert.Equal(100, storage2.Find(1).GetHeader(1));
        Assert.Equal(200, storage2.Find(1).GetHeader(2));
    }
}