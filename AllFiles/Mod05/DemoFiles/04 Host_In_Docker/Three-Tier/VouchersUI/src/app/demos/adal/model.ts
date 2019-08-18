export interface RecentFile {
  "@odata.type"?: OdataType;
  createdDateTime: string;
  id: string;
  lastModifiedDateTime: string;
  name: string;
  webUrl: string;
  size: number;
  createdBy: EdBy;
  lastModifiedBy: EdBy;
  file: File;
  fileSystemInfo: FileSystemInfo;
  remoteItem?: RecentFile;
  webDavUrl?: string;
  parentReference?: ParentReference;
  sharepointIds?: SharepointIDS;
}

export enum OdataType {
  MicrosoftGraphDriveItem = "#microsoft.graph.driveItem"
}

export interface EdBy {
  user: User;
}

export interface User {
  email: Email;
  displayName: DisplayName;
}

export enum DisplayName {
  AlexanderPajer = "Alexander Pajer"
}

export enum Email {
  AlexanderIntegrationsAt = "alexander@integrations.at",
  AlexanderPajerIntegrationsAt = "alexander.pajer@integrations.at"
}

export interface File {
  mimeType: string;
}

export interface FileSystemInfo {
  createdDateTime: string;
  lastModifiedDateTime: string;
}

export interface ParentReference {
  driveId: string;
  driveType: DriveType;
  id: string;
}

export enum DriveType {
  Business = "business",
  DocumentLibrary = "documentLibrary"
}

export interface SharepointIDS {
  listId: string;
  listItemId: string;
  listItemUniqueId: string;
  siteId: string;
  siteUrl: string;
  webId: string;
}
