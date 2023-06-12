import { Component, Injector } from "@angular/core";
import { finalize } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import {
  GithubProfile,
    GithubProfileServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { CreateGithubProfileDialogComponent } from "./create-githubProfile/create-githubProfile-dialog.component";

@Component({
  templateUrl: "./githubProfiles.component.html",
  animations: [appModuleAnimation()],
  providers: [GithubProfileServiceProxy],
})
export class GithubProfilesComponent extends PagedListingComponentBase<GithubProfile> {
  githubProfiles: GithubProfile[] = [];
  keyword: string = "";
  totalItems: number = 0;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _githubProfileService: GithubProfileServiceProxy
  ) {
    super(injector);
  }

  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this._githubProfileService
      .getList(undefined, undefined, 100)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: GithubProfile[]) => {
        this.githubProfiles = result;
        this.totalItems = result.length;
      });
  }

  protected delete(githubProfile: GithubProfile): void {
    abp.message.confirm(
      this.l("GithubProfileDeleteWarningMessage", githubProfile.username),
      undefined,
      (result: boolean) => {
        if (result) {
          this._githubProfileService
            .delete(githubProfile.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l("SuccessfullyDeleted"));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }

  createGithubProfile(): void {
    this.showCreateOrEditGithubProfileDialog();
  }

  showCreateOrEditGithubProfileDialog(id?: number): void {
    let createOrEditDependencyDialog: BsModalRef;
    if (!id) {
      createOrEditDependencyDialog = this._modalService.show(
        CreateGithubProfileDialogComponent,
        {
          class: "modal-lg",
        }
      );

      createOrEditDependencyDialog.content.onSave.subscribe(() => {
        this.refresh();
      });
    }
    // else {
    //   createOrEditApplicationDialog = this._modalService.show(
    //     EditRoleDialogComponent,
    //     {
    //       class: 'modal-lg',
    //       initialState: {
    //         id: id,
    //       },
    //     }
    //   );
  }

  editGithubProfile(githubProfile: GithubProfile): void {
    console.log("editGithubProfile");
  }
}
