//
//  MosaicAssemblyViewController.swift
//  Escapani
//
//  Created by Panos Peltekis on 3/6/21.
//

import PKHUD
import UIKit

class MosaicAssemblyViewController: BaseViewController, ViewModelable {
    
    // MARK: - ViewModelable
    
    var viewModel: MosaicAssemblyViewModel!
    
    // MARK: - Properties
    
    private var renderer: GameBoardRenderer!
    private var gameManager = GameLogicManager()
    private var isShuffling = false
    private var isAISolving = false
    private var size: Int? {
        didSet {
            configureForSize(size: size ?? 0)
        }
    }
    private var moves = 0
    private var totalSeconds: Int = 0 {
        didSet {
            timerLabel.text = totalTime(seconds: totalSeconds)
        }
    }
    private var timer: Timer?
    
    private var circleLayer = CAShapeLayer()
    private var progressLayer = CAShapeLayer()
    
    // MARK: - IBOutlets
    
    @IBOutlet weak var titleLabel: UILabel!
    @IBOutlet weak var descriptionLabel: UILabel!
    @IBOutlet weak var sixteenPiecesButton: UIButton!
    @IBOutlet weak var ninePiecesButton: UIButton!
    @IBOutlet weak var boardView: GameBoardView!
    @IBOutlet weak var helperImageView: UIImageView!
    @IBOutlet weak var timerBackgroundView: UIView!
    @IBOutlet weak var timerLabel: UILabel!
    @IBOutlet weak var solutionButton: UIButton!
    @IBOutlet weak var blurMask: UIVisualEffectView!
    
    @IBAction func startNinePiecesGame(_ sender: UIButton) {
        if size != 3 {
            self.size = 3
            gameManager.start()
        }
    }
    
    @IBAction func startSixteenPiecesGame(_ sender: UIButton) {
        if size != 4 {
            self.size = 4
            gameManager.start()
        }
    }
    
    @IBAction func restartAction(_ sender: Any?) {
        guard !isShuffling else {
            return
        }
        moves = 0
        gameManager.start()
    }
    
    @IBAction func showHelp(_ sender: Any?) {
        self.helperImageView.isHidden = false
        UIView.animate(withDuration: 0.5) {
            self.helperImageView.layer.opacity = 1
        }
    }
    
    @IBAction func hideHelp(_ sender: Any?) {
        UIView.animate(withDuration: 0.5) {
            self.helperImageView.layer.opacity = 0
        } completion: { _ in
            self.helperImageView.isHidden = true
        }
    }
    
    // MARK: - Lifecycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        titleLabel.text = "Μπορείς να συναρμολογήσεις το ψηφιδωτό?"
        descriptionLabel.text = "Επίλεξε πόσα κομμάτια θέλεις και ξεκίνα."
        
        helperImageView.image = viewModel.puzzleImage
        helperImageView.isHidden = true
        helperImageView.layer.opacity = 0
        
        timerLabel.baselineAdjustment = .alignCenters
        
        boardView.delegate = self
        gameManager.delegate = self
        
        configureForSize(size: viewModel.puzzleSize)
        gameManager.start()
    }
    
    override func viewDidLayoutSubviews() {
        super.viewDidLayoutSubviews()
        
        sixteenPiecesButton.layer.cornerRadius = sixteenPiecesButton.frame.width / 2
        ninePiecesButton.layer.cornerRadius = ninePiecesButton.frame.width / 2
        timerBackgroundView.layer.cornerRadius = timerBackgroundView.frame.width / 2
        solutionButton.layer.cornerRadius = solutionButton.frame.width / 2
        
        createCircularPath()
    }
    
    // MARK: - Methods
    
    private func configureForSize(size: Int) {
        renderer = GameBoardRenderer(boardView: boardView, size: size, image: viewModel.puzzleImage)
        gameManager.size = size
    }
    
    private func handleGameOver() {
        self.timer?.invalidate()
        progressLayer.removeAllAnimations()
        renderer.presentWinningState()
        HUD.flash(.labeledSuccess(title: ANLocalizedString("congratulations_label", comment: ""), subtitle: "\(ANLocalizedString("puzzle_moves_first_part_label", comment: "")) \(self.moves) \(ANLocalizedString("puzzle_moves_second_part_label", comment: ""))"), delay: popupDelay, completion: { _ in
            self.solutionButton.isUserInteractionEnabled = false
            self.showHelp(nil)
        })
    }
    
    private func toggleBlurMask(show: Bool) {
        UIView.animate(withDuration: 0.2) {
            self.blurMask.alpha = show ? 0.5 : 0.0
        }
    }
    
    private func totalTime(seconds: Int) -> String {
        let hours = seconds / 3600
        let minutes = seconds / 60 % 60
        let seconds = seconds % 60
        
        if (hours != 0 && minutes != 0 && seconds != 0) {
            return String(format:"%02i:%02i:%02i", hours, minutes, seconds)
        } else if (hours == 0 && minutes != 0 && seconds != 0) {
            return String(format:"%02i:%02i", minutes, seconds)
        } else if (hours == 0 && minutes != 0 && seconds == 0) {
            progressAnimation(duration: 60)
            return String(format:"%02i:00", minutes, seconds)
        } else {
            return String(format:"%01i", seconds)
        }
    }
    
    @objc
    func fireTimer() {
        self.totalSeconds += 1
    }
    
    func createCircularPath() {
        let circularPath = UIBezierPath(arcCenter: CGPoint(x: timerBackgroundView.frame.size.width / 2.0, y: timerBackgroundView.frame.size.height / 2.0), radius: timerBackgroundView.frame.size.height / 2.0, startAngle: -.pi / 2, endAngle: 3 * .pi / 2, clockwise: true)
        circleLayer.path = circularPath.cgPath
        circleLayer.fillColor = UIColor.clear.cgColor
        circleLayer.lineCap = .round
        circleLayer.lineWidth = 10.0
        circleLayer.strokeColor = UIColor.black.cgColor
        progressLayer.path = circularPath.cgPath
        progressLayer.fillColor = UIColor.clear.cgColor
        progressLayer.lineCap = .round
        progressLayer.lineWidth = 5.0
        progressLayer.strokeEnd = 0
        progressLayer.strokeColor = viewModel.theme.mainLightBlueColor.cgColor
        timerBackgroundView.layer.addSublayer(circleLayer)
        timerBackgroundView.layer.addSublayer(progressLayer)
    }
    
    func progressAnimation(duration: TimeInterval) {
        let circularProgressAnimation = CABasicAnimation(keyPath: "strokeEnd")
        circularProgressAnimation.duration = duration
        circularProgressAnimation.toValue = 1.0
        circularProgressAnimation.fillMode = .forwards
        circularProgressAnimation.isRemovedOnCompletion = false
        progressLayer.add(circularProgressAnimation, forKey: "progressAnim")
    }
    
}

extension MosaicAssemblyViewController: Storyboardable {
    static var storyboardName: String = "MosaicAssembly"
    static var storyboardIdentifier: String? = "MosaicAssemblyViewControllerIdentifier"
}

extension MosaicAssemblyViewController: GameBoardViewDelegate {
    func gameBoardView(view: GameBoardView, didSwipeInDirection direction: ShiftDirection) {
        moves += 1
        gameManager.shift(isSettingUp: false, direction: direction, completionBlock: {
            if self.gameManager.checkIfGameOver() {
                self.handleGameOver()
            }
        })
    }
}

extension MosaicAssemblyViewController: GameLogicManagerDelegate {
    func gameLogicManagerDidAISolve(moves: Int) {
        
    }
    
    func gameLogicManagerDidEnableInteractions() {
        boardView.isUserInteractionEnabled = true
    }
    
    func gameLogicManagerDidDisableInteractions() {
        boardView.isUserInteractionEnabled = false
    }
    
    func gameLogicManagerDidStartShuffle() {
        isShuffling = true
        toggleBlurMask(show: true)
        progressLayer.removeAllAnimations()
        self.timer?.invalidate()
        self.timerLabel.text = ""
    }
    
    func gameLogicManagerDidEndShuffle() {
        isShuffling = false
        toggleBlurMask(show: false)
        totalSeconds = 0
        self.timer = Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(fireTimer), userInfo: nil, repeats: true)
        progressAnimation(duration: 60)
        hideHelp(nil)
    }
    
    func gameLogicManagerDidSetTiles(tiles: [Tile]) {
        renderer.setTiles(tiles: tiles)
    }
    
    func gameLogicManagerDidSwapTiles(isSettingUp: Bool, firstTile: Tile, secondTile: Tile, speed: TimeInterval, completionBlock: @escaping () -> Void) {
        renderer.swapTiles(isSettingUp: isSettingUp, firstTile: firstTile, secondTile: secondTile, speed: speed, completionBlock: completionBlock)
    }
}

extension UIImage {
    func cropImage(toRect cropRect: CGRect, viewWidth: CGFloat, viewHeight: CGFloat) -> UIImage? {
        let imageViewScale = max(self.size.width / viewWidth,
                                 self.size.height / viewHeight)

        // Scale cropRect to handle images larger than shown-on-screen size
        let cropZone = CGRect(x:cropRect.origin.x * imageViewScale,
                              y:cropRect.origin.y * imageViewScale,
                              width:cropRect.size.width * imageViewScale,
                              height:cropRect.size.height * imageViewScale)

        // Perform cropping in Core Graphics
        guard let cutImageRef: CGImage = self.cgImage?.cropping(to:cropZone) else { return nil }

        // Return image to UIImage
        let croppedImage: UIImage = UIImage(cgImage: cutImageRef)
        return croppedImage
    }
    
    func cropImage(toRect rect:CGRect) -> UIImage? {
        var rect = rect
        rect.origin.y = rect.origin.y * self.scale
        rect.origin.x = rect.origin.x * self.scale
        rect.size.width = rect.width * self.scale
        rect.size.height = rect.height * self.scale

        guard let imageRef = self.cgImage?.cropping(to: rect) else {
            return nil
        }

        let croppedImage = UIImage(cgImage:imageRef)
        return croppedImage
    }
}
