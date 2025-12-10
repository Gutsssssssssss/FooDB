namespace FooDB;

public class Block : IBlock
{
    private readonly byte[] firstSector;
    private readonly long?[] cachedHeaderValue = new long?[5];
    private readonly Stream stream;
    private readonly BlockStorage storage;
    private readonly uint id;
    
    private bool isFirstSectorDirty = false;
    private bool isDisposed = false;
    
    public event EventHandler Disposed;

    public uint Id => id;

    public Block(byte[] firstSector, Stream stream, BlockStorage storage, uint id)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));
        
        if (firstSector == null) throw new ArgumentNullException(nameof(firstSector));
        
        if (firstSector.Length != storage.DiskSectorSize) throw new ArgumentException("Invalid block size", nameof(firstSector));

        this.firstSector = firstSector;
        this.stream = stream;
        this.storage = storage;
        this.id = id;
    }

    public long GetHeader(int field)
    {
        throw new NotImplementedException();
    }
    
    public void SetHeader(int field, long value)
    {
        throw new NotImplementedException();
    }
    
    public void Read(byte[] dst, int dstOffset, int srcOffset, int count)
    {
        throw new NotImplementedException();
    }
    
    public void Write(byte[] src, int srcOffset, int dstOffset, int count)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}