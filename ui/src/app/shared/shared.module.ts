import { NgModule } from "@angular/core";
import { FileSizePipe } from "./pipes/file-size.pipe";

@NgModule({
    declarations: [FileSizePipe],
    exports: [FileSizePipe]
})
export class SharedModule {

}