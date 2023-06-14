import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  Input,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { AppComponentBase } from "@shared/app-component-base";
import {
  PermissionDto,
  GithubRepositoryServiceProxy,
  CreateGithubRepositoryDto,
  GithubRepository,
  GithubProfileServiceProxy,
  GithubProfile,
} from "@shared/service-proxies/service-proxies";
import { forEach as _forEach, map as _map } from "lodash-es";

@Component({
  templateUrl: "create-githubRepository-dialog.component.html",
  providers: [GithubRepositoryServiceProxy, GithubProfileServiceProxy],
})
export class CreateGithubRepositoryDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  githubRepository = new CreateGithubRepositoryDto();
  permissions: PermissionDto[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;
  githubProfiles: GithubProfile[] = [];
  githubProfileId: number;
  githubRepositoryId: number = null;

  @Input() applicationId: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _githubRepositoryService: GithubRepositoryServiceProxy,
    private _githubProfileService: GithubProfileServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._githubProfileService
      .getList(undefined, undefined, undefined)
      .subscribe((result) => {
        this.githubProfiles = result;
      });

    this.findGithubRepositoryForApplicationId();
  }

  save(): void {
    this.saving = true;

    const githubRepository = new CreateGithubRepositoryDto();
    githubRepository.init(this.githubRepository);

    githubRepository.applicationId = this.applicationId;

    this._githubRepositoryService.create(githubRepository).subscribe(
      () => {
        this.notify.info(this.l("SavedSuccessfully"));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }

  onGithubProfileChange(): void {
    this.githubRepository.githubProfileId = this.githubProfileId;
    console.log(this.githubRepository);
  }

  findGithubRepositoryForApplicationId(): void {
    this._githubRepositoryService
      .findGithubRepositoryForApplicationId(this.applicationId)
      .subscribe((result) => {
        this.githubRepositoryId = result.id;
      });
  }

  upload(): void {
    this.saving = true;

    let githubRepository = this._githubRepositoryService
      .get(this.githubRepositoryId)
      .subscribe((result) => {
        return result;
      });

    this._githubRepositoryService.generateAndUploadGithubRepository(
      this.githubRepository
    );
  }
}
