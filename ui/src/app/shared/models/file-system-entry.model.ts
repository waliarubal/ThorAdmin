export interface IFileSystemEntry {
    IsDirectory: boolean;
    IsReadOnly: boolean;
    Name: string;
    Path: string;
    Created: Date;
    Modified: Date;
    Size: number;
    Children: IFileSystemEntry[];
}