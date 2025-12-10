namespace FooDB;

public interface IBlockStorage
{
    int BlockContentSize { get; }
    int BlockHeaderSize { get; }
    int BlockSize { get; }
    IBlock Find(uint blockId);
    IBlock CreateNew();
}

public interface IBlock : IDisposable
{
    uint Id { get; }
    long GetHeader(int field);
    void SetHeader(int field, long value);
    void Read(byte[] dst, int dstOffset, int srcOffset, int count);
    void Write(byte[] src, int srcOffset, int dstOffset, int count);
}