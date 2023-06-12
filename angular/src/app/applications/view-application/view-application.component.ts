import { Component, Injector } from "@angular/core";
import { finalize } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import {
  ApplicationDto,
  ApplicationServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { CreateApplicationDialogComponent } from "../create-application/create-application-dialog.component";
import { CLIENT_RENEG_LIMIT } from "tls";
import { Moment } from "moment";

@Component({
    templateUrl: "./view-application.component.html",
    animations: [appModuleAnimation()],
})
export class ViewApplicationComponent {
    application: ApplicationDto = new ApplicationDto();
    id: number;
    constructor(
        injector: Injector,
        private _modalService: BsModalService,
        private _applicationsService: ApplicationServiceProxy
    ) {
    }

    ngOnInit(): void {
        // get id from url
        this.id = Number(window.location.pathname.split("/")[3]);
        this._applicationsService.getDetails(this.id).subscribe((result: ApplicationDto) => {
            this.application = result;
            console.log(this.application);
        });
    }

    downloadApplication() {
        console.log("download");
    }

    protected delete(): void {
      abp.message.confirm(
        `Are you sure you want to delete ${this.application.name}?`,
        undefined,
        (result: boolean) => {
          if (result) {
            this._applicationsService
              .delete(this.id)
              .pipe(
                finalize(() => {
                  abp.notify.success("SuccessfullyDeleted");
                })
              )
              .subscribe(() => {});
          }
        }
      );
    }

    edit(): void {
      console.log("edit");
    }

    uploadToGithub() {
      console.log("upload");
    }

    formatDate(date: Moment): string {
      return date.format("DD-MM-YYYY HH:mm");
    }
}