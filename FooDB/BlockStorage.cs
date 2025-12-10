namespace FooDB;

public class BlockStorage : IBlockStorage
{
    private readonly Stream stream;
    private readonly int blockSize;
    private readonly int blockHeaderSize;
    private readonly int blockContentSize;
    private readonly int unitOfWork;
    private readonly Dictionary<uint, Block> blocks = new Dictionary<uint, Block>();

    public int DiskSectorSize => unitOfWork;
    
    public int BlockSize => blockSize;
    
    public int BlockHeaderSize => blockHeaderSize;
    
    public int BlockContentSize => blockContentSize;

    public BlockStorage(Stream storage, int blockSize = 40960, int blockHeaderSize = 48)
    {
        if (storage == null) throw new ArgumentNullException(nameof(storage));

        if (blockHeaderSize >= blockSize) throw new ArgumentException("blockHeaderSize cannot be larger than or equal to blockSize");
        
        if (blockSize < 128)  throw new ArgumentException("blockSize too small");

        this.unitOfWork = ((blockSize >= 4096) ? 4066 : 128);
        this.stream = storage;
        this.blockSize = blockSize;
        this.blockHeaderSize = blockHeaderSize;
        this.blockContentSize = blockSize - blockHeaderSize;
    }

    public IBlock Find(uint blockId)
    {
        throw new NotImplementedException();
    }
    
    public IBlock CreateNew()
    {
        if ((this.stream.Length % blockSize) != 0) throw new DataMisalignedException("Unexpected stream length: " + this.stream.Length);
        
        var blockId = (uint)Math.Ceiling((double)this.stream.Length / (double)blockSize);

        this.stream.SetLength((long)(blockId * blockSize) + blockSize);
        this.stream.Flush();
        
        var block = new Block(new byte[DiskSectorSize], this.stream, this, blockId);
        OnBlockInitialized(block);
        return block;
    }

    protected virtual void OnBlockInitialized(Block block)
    {
        blocks[block.Id] = block;
        block.Disposed += HandleBlockDisposed;
    }

    protected virtual void HandleBlockDisposed(object sender, EventArgs e)
    {
        var block = (Block)sender;
        block.Disposed -= HandleBlockDisposed;

        blocks.Remove(block.Id);
    }

}