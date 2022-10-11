import { environment } from '../../environments/environment';

const API_BASE = environment.apiBase;

export enum Bool {
    Yes = 'yes',
    No = 'no'
}

export const PATH_SEPARATOR = '/';

export const API_GET_INSTANCES = `${API_BASE}/WordPress/GetInstances`;
export const API_GET_INSTANCE = `${API_BASE}/WordPress/GetInstance`;
export const API_CREATE_INSTANCE = `${API_BASE}/WordPress/CreateInstance`;
export const API_DELETE_INSTANCE = `${API_BASE}/WordPress/DeleteInstance`;
export const API_GET_ENTRIES = `${API_BASE}/FileSystem/GetEntries`;
export const API_GET_PARENT_ENTRY = `${API_BASE}/FileSystem/GetParentEntry`;
export const API_DELETE_ENTRY = `${API_BASE}/FileSystem/DeleteEntry`;
export const API_RENAME_ENTRY = `${API_BASE}/FileSystem/RenameEntry`;
export const API_CREATE_ENTRY = `${API_BASE}/FileSystem/CreateEntry`;
export const API_DOWNLOAD_ENTRY = `${API_BASE}/FileSystem/DownloadEntry`;
export const API_UPLOAD_ENTRY = `${API_BASE}/FileSystem/UploadEntry`;
export const API_GET_PROCESSES = `${API_BASE}/MachineInfo/GetProcesses`;
export const API_KILL_PROCESSES = `${API_BASE}/MachineInfo/KillProcess`;
