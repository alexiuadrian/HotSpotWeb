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
  GithubRepositoryServiceProxy
} from "@shared/service-proxies/service-proxies";
import { CreateApplicationDialogComponent } from "../create-application/create-application-dialog.component";
import { CLIENT_RENEG_LIMIT } from "tls";
import { Moment } from "moment";
import { CreateGithubRepositoryDialogComponent } from "@app/githubRepositories/create-githubRepository/create-githubRepository-dialog.component";

@Component({
    templateUrl: "./view-application.component.html",
    animations: [appModuleAnimation()],
    providers: [GithubRepositoryServiceProxy]
})
export class ViewApplicationComponent {
    application: ApplicationDto = new ApplicationDto();
    id: number;
    isApplicationOnGithubStatus: number = 0;


    constructor(
        injector: Injector,
        private _modalService: BsModalService,
        private _applicationsService: ApplicationServiceProxy,
        private _githubRepositoryService: GithubRepositoryServiceProxy
    ) {
    }

    ngOnInit(): void {
        // get id from url
        this.id = Number(window.location.pathname.split("/")[3]);
        this._applicationsService.getDetails(this.id).subscribe((result: ApplicationDto) => {
            this.application = result;
            this.isOnGithub();
            console.log(this.isApplicationOnGithubStatus)
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

    formatDate(date: Moment): string {
      return date.format("DD-MM-YYYY HH:mm");
    }

    addToGithub(): void {
      this.showCreateGithubRepositoryDialog();
    }
  
    showCreateGithubRepositoryDialog(id?: number): void {
      let createGithubRepositoryDialog: BsModalRef;
      if (!id) {
        createGithubRepositoryDialog = this._modalService.show(
          CreateGithubRepositoryDialogComponent,
          {
            class: "modal-lg",
            initialState: {
              applicationId: this.id
            }
          }
        );
      }
      // else {
      //   createGithubRepositoryDialog = this._modalService.show(
      //     EditRoleDialogComponent,
      //     {
      //       class: 'modal-lg',
      //       initialState: {
      //         id: id,
      //       },
      //     }
      //   );
    }

    isOnGithub(): void {
      this._githubRepositoryService.isApplicationOnGithub(this.id).subscribe((result: number) => {
        this.isApplicationOnGithubStatus = result;
      });
    }
}